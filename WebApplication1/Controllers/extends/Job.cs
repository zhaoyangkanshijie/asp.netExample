using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Hosting;

namespace Quartz.net
{
    public class Job : IInterruptableJob
    {
        private Thread _currentThread;

        public void Execute(IJobExecutionContext context)
        {
            _currentThread = Thread.CurrentThread;

            try
            {
                //执行计划
                string path = HostingEnvironment.MapPath("~/Content/test/");//获取上传路径的物理地址 
                if (!Directory.Exists(path))//判断文件夹是否存在 
                {
                    Directory.CreateDirectory(path);//不存在则创建文件夹 
                }
                FileStream myfs;
                if (System.IO.File.Exists(path + "a.txt"))
                {
                    myfs = new FileStream(path + "a.txt", FileMode.Append);
                }
                else
                {
                    myfs = new FileStream(path + "a.txt", FileMode.OpenOrCreate);
                }
                //创建写入器  
                StreamWriter mySw = new StreamWriter(myfs);//将文件流给写入器  
                //将录入的内容写入文件  
                mySw.Write(DateTime.Now.ToString() + "\r\n");
                //关闭写入器  
                mySw.Close();
                //关闭文件流  
                myfs.Close();  
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _currentThread = null;
            }
        }

        public void Interrupt()
        {
            if (_currentThread != null)
            {
                _currentThread.Abort();
                _currentThread = null;
            }
        }
    }
}