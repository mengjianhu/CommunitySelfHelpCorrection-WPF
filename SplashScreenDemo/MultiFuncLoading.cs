﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SplashScreenDemo
{
    public partial class MultiFuncLoading : Form
    {
        //保存父窗口信息，主要用于居中显示加载窗体
        private Form partentForm=null;
        public MultiFuncLoading(Form partentForm)
        {
            InitializeComponent();
            this.partentForm = partentForm;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            //设置一些Loading窗体信息
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            this.Text = "有进度，有提示，有取消按钮 Loading...";
            // 下面的方法用来使得Loading窗体居中父窗体显示
            int parentForm_Position_x = this.partentForm.Location.X;
            int parentForm_Position_y = this.partentForm.Location.Y;
            int parentForm_Width = this.partentForm.Width;
            int parentForm_Height = this.partentForm.Height;

            int start_x = (int)(parentForm_Position_x + (parentForm_Width - this.Width) / 2);
            int start_y = (int)(parentForm_Position_y + (parentForm_Height - this.Height) / 2);
            this.Location = new System.Drawing.Point(start_x, start_y);
            
        }

        ///// <summary>
        ///// 改变Loading的进度
        ///// </summary>
        ///// <param name="percent"></param>
        public void SetTxt(string title="Loading...",string lbl1= "加载中，请稍等...", string lbl2= "Please Waitting...")
        {
            // 采用Invoke形式进行操作
            this.Invoke(new MethodInvoker(() =>
            {
                this.Text = title;
                this.lbl_tips.Text = lbl1;
                this.lbl_tips_son.Text = lbl2;
            }));
        }
        public void SetJD(string JDStr,string curstr)
        {
            // 采用Invoke形式进行操作
            this.Invoke(new MethodInvoker(() =>
            {
                this.lbl_jd.Text = JDStr;
                this.lbl_cur.Text = curstr;
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Demo.flag = false;
        }
    }
}
