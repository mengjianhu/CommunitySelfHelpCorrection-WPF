using log4net;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperUnit
{
    class SM2
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(SM2));

        private ECPrivateKeyParameters privateKeyParameters;


        private ECPublicKeyParameters publicKeyParameters;

        private SM2Param sm2Param;

        private static X9ECParameters x9ECParameters = GMNamedCurves.GetByName("sm2p256v1");

        public SM2(string priKey, string pubKey)
        {
            this.init(HexUtils.decode(priKey), HexUtils.decode(pubKey));
        }

        private void init(byte[] priKey, byte[] pubKey)
        {
            this.sm2Param = new SM2Param();
            if (null != priKey && this.privateKeyParameters == null)
            {
                this.privateKeyParameters = new ECPrivateKeyParameters(new BigInteger(1, priKey), this.sm2Param.ecc_bc_spec);
            }
            if (null != pubKey && this.publicKeyParameters == null)
            {
                this.publicKeyParameters = new ECPublicKeyParameters(this.sm2Param.ecc_curve.DecodePoint(pubKey), this.sm2Param.ecc_bc_spec);

            }
        }
        #region  测试

        private const int RS_LEN = 32;

        public static byte[] BigIntToFixexLengthBytes(BigInteger rOrS)
        {
            // for sm2p256v1, n is 00fffffffeffffffffffffffffffffffff7203df6b21c6052b53bbf40939d54123,
            // r and s are the result of mod n, so they should be less than n and have length<=32
            byte[] rs = rOrS.ToByteArray();
            if (rs.Length == RS_LEN) return rs;
            else if (rs.Length == RS_LEN + 1 && rs[0] == 0) return Arrays.CopyOfRange(rs, 1, RS_LEN + 1);
            else if (rs.Length < RS_LEN)
            {
                byte[] result = new byte[RS_LEN];
                Arrays.Fill(result, (byte)0);
                Buffer.BlockCopy(rs, 0, result, RS_LEN - rs.Length, rs.Length);
                return result;
            }
            else
            {
                throw new ArgumentException("err rs: " + Hex.ToHexString(rs));
            }
        }
        public static byte[] RsAsn1ToByteArray(byte[] rsDer)
        {
            Asn1Sequence seq = Asn1Sequence.GetInstance(rsDer);
            byte[] r = BigIntToFixexLengthBytes(DerInteger.GetInstance(seq[0]).Value);
            byte[] s = BigIntToFixexLengthBytes(DerInteger.GetInstance(seq[1]).Value);
            byte[] result = new byte[32 * 2];
            Buffer.BlockCopy(r, 0, result, 0, r.Length);
            Buffer.BlockCopy(s, 0, result, 32, s.Length);
            return result;
        }
        /**
         * BC的SM3withSM2签名得到的结果的rs是asn1格式的，这个方法转化成直接拼接r||s
         * @param rsDer rs in asn1 format
         * @return sign result in plain byte array
         */
        public static byte[] RsAsn1ToPlainByteArray(byte[] rsDer)
        {
            Asn1Sequence seq = Asn1Sequence.GetInstance(rsDer);
            byte[] r = BigIntToFixexLengthBytes(DerInteger.GetInstance(seq[0]).Value);
            byte[] s = BigIntToFixexLengthBytes(DerInteger.GetInstance(seq[1]).Value);
            byte[] result = new byte[RS_LEN * 2];
            Buffer.BlockCopy(r, 0, result, 0, r.Length);
            Buffer.BlockCopy(s, 0, result, RS_LEN, s.Length);
            return result;
        }

        /**
         * BC的SM3withSM2验签需要的rs是asn1格式的，这个方法将直接拼接r||s的字节数组转化成asn1格式
         * @param sign in plain byte array
         * @return rs result in asn1 format
         */
        public static byte[] RsPlainByteArrayToAsn1(byte[] sign)
        {
            if (sign.Length != RS_LEN * 2) throw new ArgumentException("err rs. ");
            BigInteger r = new BigInteger(1, Arrays.CopyOfRange(sign, 0, RS_LEN));
            BigInteger s = new BigInteger(1, Arrays.CopyOfRange(sign, RS_LEN, RS_LEN * 2));
            Asn1EncodableVector v = new Asn1EncodableVector();
            v.Add(new DerInteger(r));
            v.Add(new DerInteger(s));
            try
            {
                return new DerSequence(v).GetEncoded("DER");
            }
            catch (IOException e)
            {
                log.Error("RsPlainByteArrayToAsn1 error: " + e.Message, e);
                return null;
            }
        }

        #endregion

        /**
         * 加签
         */
        public string Sign(string data)
        {
            byte[] msg = HexUtils.hexStrToByte(data);
            SM2Signer sm2Signer = new SM2Signer();
            sm2Signer.Init(true, this.privateKeyParameters);
            sm2Signer.BlockUpdate(msg, 0, msg.Length);
            return Hex.ToHexString(sm2Signer.GenerateSignature());
        }
        public string SignNoAsn1(string data)
        {
            byte[] msg = HexUtils.hexStrToByte(data);
            SM2Signer sm2Signer = new SM2Signer();
            sm2Signer.Init(true, this.privateKeyParameters);
            sm2Signer.BlockUpdate(msg, 0, msg.Length);
            return Hex.ToHexString(RsAsn1ToPlainByteArray(sm2Signer.GenerateSignature()));
        }

        /*
         * 验签
         */
        public bool verifySign(string data, string sign)
        {
            byte[] signHex = Hex.Decode(sign);
            byte[] msgByte = Hex.Decode(data);
            SM2Signer sm2Signer = new SM2Signer();
            sm2Signer.Init(false, this.publicKeyParameters);
            sm2Signer.BlockUpdate(msgByte, 0, msgByte.Length);
            return sm2Signer.VerifySignature(signHex);
        }

        /**
         * 加密
         * 
         */
        public byte[] encrypt(byte[] plainText)
        {
            SM2Engine engine = new SM2Engine();
            engine.Init(true, new ParametersWithRandom(this.publicKeyParameters));
            return ChangeC1C2C3ToC1C3C2(engine.ProcessBlock(plainText, 0, plainText.Length));
        }

        /**
        * bc加解密使用旧标c1||c2||c3，此方法在加密后调用，将结果转化为c1||c3||c2
        * @param c1c2c3
        * @return
        */
        private static byte[] ChangeC1C2C3ToC1C3C2(byte[] c1c2c3)
        {
            int c1Len = (x9ECParameters.Curve.FieldSize + 7) / 8 * 2 + 1; //sm2p256v1的这个固定65。可看GMNamedCurves、ECCurve代码。
            const int c3Len = 32; //new SM3Digest().getDigestSize();
            byte[] result = new byte[c1c2c3.Length];
            Buffer.BlockCopy(c1c2c3, 0, result, 0, c1Len); //c1
            Buffer.BlockCopy(c1c2c3, c1c2c3.Length - c3Len, result, c1Len, c3Len); //c3
            Buffer.BlockCopy(c1c2c3, c1Len, result, c1Len + c3Len, c1c2c3.Length - c1Len - c3Len); //c2
            return result;
        }

        /**
         * 
         * 解密
         * 
         */
        public byte[] deceypt(byte[] plainText)
        {
            byte[] plain = ChangeC1C3C2ToC1C2C3(plainText);
            SM2Engine engine = new SM2Engine();
            engine.Init(false, this.privateKeyParameters);
            return engine.ProcessBlock(plain, 0, plain.Length);
        }

        /**
        * bc加解密使用旧标c1||c3||c2，此方法在解密前调用，将密文转化为c1||c2||c3再去解密
        * @param c1c3c2
        * @return
        */
        private static byte[] ChangeC1C3C2ToC1C2C3(byte[] c1c3c2)
        {
            int c1Len = (x9ECParameters.Curve.FieldSize + 7) / 8 * 2 + 1; //sm2p256v1的这个固定65。可看GMNamedCurves、ECCurve代码。
            const int c3Len = 32; //new SM3Digest().GetDigestSize();
            byte[] result = new byte[c1c3c2.Length];
            Buffer.BlockCopy(c1c3c2, 0, result, 0, c1Len); //c1: 0->65
            Buffer.BlockCopy(c1c3c2, c1Len + c3Len, result, c1Len, c1c3c2.Length - c1Len - c3Len); //c2
            Buffer.BlockCopy(c1c3c2, c1Len, result, c1c3c2.Length - c3Len, c3Len); //c3
            return result;
        }


        public static string[] GenerateKeyPair()
        {
            AsymmetricCipherKeyPair kPair = genCipherKeyPair();
            ECPrivateKeyParameters ecPrivateKey = (ECPrivateKeyParameters)kPair.Private;
            ECPublicKeyParameters ecPublicKey = (ECPublicKeyParameters)kPair.Public;
            BigInteger priKey = ecPrivateKey.D;
            ECPoint pubKey = ecPublicKey.Q;
            byte[] priByte = priKey.ToByteArray();
            byte[] pubByte = pubKey.GetEncoded(false);
            if (priByte.Length == 33)
            {
                log.Debug("private key size 33");
                byte[] newPriByte = new byte[32];
                Array.Copy(priByte, 1, newPriByte, 0, 32);
                priByte = newPriByte;
            }
            string[] keyPairs = new string[] { Hex.ToHexString(pubByte), Hex.ToHexString(priByte) };
            return keyPairs;
        }


        /**
         *      生成引用
         * */
        private static AsymmetricCipherKeyPair genCipherKeyPair()
        {
            SM2Param ecc_param = new SM2Param();
            ECDomainParameters ecDomainParamters = ecc_param.ecc_bc_spec;
            ECKeyGenerationParameters ecGenParam = new ECKeyGenerationParameters(ecDomainParamters, new SecureRandom());

            ECKeyPairGenerator ecKeyPairGenerator = new ECKeyPairGenerator();
            ecKeyPairGenerator.Init(ecGenParam);
            return ecKeyPairGenerator.GenerateKeyPair();
        }

        private class SM2Param
        {
            public static String[] ecc_param = { "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF", "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC", "28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93", "FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123", "32C4AE2C1F1981195F9904466A39C9948FE30BBFF2660BE1715A4589334C74C7", "BC3736A2F4F6779C59BDCEE36B692153D0A9877CC62A474002DF32E52139F0A0" };
            public BigInteger ecc_p;
            public BigInteger ecc_a;
            public BigInteger ecc_b;
            public BigInteger ecc_n;
            public BigInteger ecc_gx;
            public BigInteger ecc_gy;
            public ECCurve ecc_curve;
            public ECDomainParameters ecc_bc_spec;

            public SM2Param()
            {
                this.ecc_p = new BigInteger(ecc_param[0], 16);
                this.ecc_a = new BigInteger(ecc_param[1], 16);
                this.ecc_b = new BigInteger(ecc_param[2], 16);
                this.ecc_n = new BigInteger(ecc_param[3], 16);
                this.ecc_gx = new BigInteger(ecc_param[4], 16);
                this.ecc_gy = new BigInteger(ecc_param[5], 16);
                this.ecc_curve = new FpCurve(ecc_p, ecc_a, ecc_b, ecc_n, BigInteger.One);
                this.ecc_bc_spec = new ECDomainParameters(this.ecc_curve, this.ecc_curve.CreatePoint(this.ecc_gx, this.ecc_gy), this.ecc_n);
            }
        }
    }
}
