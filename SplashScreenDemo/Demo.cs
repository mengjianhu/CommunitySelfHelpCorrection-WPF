using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplashScreenDemo
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }

        //演示调用方法
        private void button1_Click(object sender, EventArgs e)
        {
            SimpleLoading loadingfrm = new SimpleLoading(this);
            //将Loaing窗口，注入到 SplashScreenManager 来管理
            GF2Koder.SplashScreenManager loading = new GF2Koder.SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            //try catch 包起来，防止出错
            try
            {
                //模拟耗时操作
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                }
                
            }
            catch (Exception) { /*可选处理异常*/ }
            finally { loading.CloseWaitForm(); }
        }


        public static bool flag = true;
        private void button2_Click(object sender, EventArgs e)
        {
            flag = true;//flag 为false时候，退出执行耗时操作

            MultiFuncLoading loadingfrm = new MultiFuncLoading(this);
            // 将Loaing窗口，注入到 SplashScreenManager 来管理
            GF2Koder.SplashScreenManager loading = new GF2Koder.SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            // 设置loadingfrm操作必须在调用ShowLoading之后执行
            loadingfrm.SetTxt("多功能Loaidng界面", "拼命加载中...客官耐心等待", "Please Waitting...");
            // try catch 包起来，防止出错
            try
            {
                //模拟耗时操作
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    loadingfrm.SetJD("当前："+i+"/总计：100","当前进度："+i);
                    if (!flag) { break;/*用户点击取消执行后，跳出循环*/ }
                }

            }
            catch (Exception) { /*可选处理异常*/ }
            finally { loading.CloseWaitForm(); }
        }
    }
}
