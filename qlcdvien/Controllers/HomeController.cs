using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace qlcdvien.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult DownloadFile()
        {


            string file = BackupRestore.BackupDatabase();
            string contentType = "application/octet-stream";
            var fileName = Path.GetFileName(file);
            
            return File(file, contentType, fileName);


        }

        
        
            public ActionResult RestoreDatabase()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Upload(HttpPostedFileBase file)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    //var path = Path.Combine(Server.MapPath("~/Images/Database/"), fileName);
                    file.SaveAs(BackupRestore.Getdatabases("(local)").BackupDirectory + "\\FullBackUp.bak" );
                BackupRestore.RestoreDatabase();
                
            }


            
            return RedirectToAction("RestoreDatabase");
            }
        

     


        public class BackupRestore
        {
            static Server srv;
            static ServerConnection conn;

            public static string BackupDatabase(string serverName="(local)", string databaseName="qlcdv", string filePath= "FullBackUp.bak")
            {
                

                conn = new ServerConnection();
                conn.ServerInstance = serverName;
                srv = new Server(conn);
                System.IO.File.Delete(srv.BackupDirectory + "\\FullBackUp.bak");
                string fullPath= srv.BackupDirectory + "\\FullBackUp.bak"; 
                try
                {
                    Backup bkp = new Backup();

                    bkp.Action = BackupActionType.Database;
                    bkp.Database = databaseName;

                    bkp.Devices.AddDevice(filePath, DeviceType.File);
                    bkp.Incremental = false;

                    bkp.SqlBackup(srv);

                    conn.Disconnect();
                    conn = null;
                    srv = null;
                }

                catch (SmoException ex)
                {
                    throw new SmoException(ex.Message, ex.InnerException);
                }
                catch (IOException ex)
                {
                    throw new IOException(ex.Message, ex.InnerException);
                }
                return fullPath;
            }

            public static void RestoreDatabase(string serverName = "(local)", string databaseName = "qlcdv", string filePath = "FullBackUp.bak")
            {

                conn = new ServerConnection();
                conn.ServerInstance = serverName;
                srv = new Server(conn);

                try
                {
                    Restore res = new Restore();

                    res.Devices.AddDevice(filePath, DeviceType.File);

                    RelocateFile DataFile = new RelocateFile();
                    string MDF = res.ReadFileList(srv).Rows[0][1].ToString();
                    DataFile.LogicalFileName = res.ReadFileList(srv).Rows[0][0].ToString();
                    DataFile.PhysicalFileName = srv.Databases[databaseName].FileGroups[0].Files[0].FileName;

                    RelocateFile LogFile = new RelocateFile();
                    string LDF = res.ReadFileList(srv).Rows[1][1].ToString();
                    LogFile.LogicalFileName = res.ReadFileList(srv).Rows[1][0].ToString();
                    LogFile.PhysicalFileName = srv.Databases[databaseName].LogFiles[0].FileName;

                    res.RelocateFiles.Add(DataFile);
                    res.RelocateFiles.Add(LogFile);

                    res.Database = databaseName;
                    res.NoRecovery = false;
                    res.ReplaceDatabase = true;
                    res.SqlRestore(srv);
                    conn.Disconnect();
                }
                catch (SmoException ex)
                {
                    throw new SmoException(ex.Message, ex.InnerException);
                }
                catch (IOException ex)
                {
                    throw new IOException(ex.Message, ex.InnerException);
                }
            }

            public static Server Getdatabases(string serverName)
            {
                conn = new ServerConnection();
                conn.ServerInstance = serverName;

                srv = new Server(conn);
                conn.Disconnect();
                return srv;

            }
        }
    }
}