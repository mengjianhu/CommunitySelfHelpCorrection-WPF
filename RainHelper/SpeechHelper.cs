using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace RainHelper
{
    public class SpeechHelper
    {
        private SpeechSynthesizer speech;
        /// <summary>
        /// 音量
        /// </summary>
        private int value = 100;
        /// <summary>
        /// 语速
        /// </summary>
        private int rate = 1;

        public SpeechHelper()
        {
            speech = new SpeechSynthesizer();
        }
        public void Speak(string text)
        {
           
            speech.Rate = rate;
            speech.SelectVoice("Microsoft Huihui Desktop");//设置播音员（中文）
            //speech.SelectVoice("Microsoft Anna"); //英文
            speech.Volume = value;
         
            speech.SpeakAsync(text);//语音阅读方法 
        }



    }
}
