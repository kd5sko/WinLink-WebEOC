using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Winlink_Importer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["IncidentSelected"] = "";
            serverInfo.InnerHtml = "<h4>Server Info:<i>" + ConfigurationManager.AppSettings["WebEOCServerURL"]+"</i></h4></br>";

            try
            {
                WebEOC.API api = new WebEOC.API();
                api.Url = ConfigurationManager.AppSettings["WebEOCServerURL"];

                string[] incidents = api.GetIncidents(creds());

                foreach (string incident in incidents)
                {
                    incidentSelect.Items.Add(new ListItem(incident));
                }
            }
            catch (Exception)
            {
                Session["Message"] = "Check Creds or url for WebEOC in the web.config file";
                Response.Redirect("Error.aspx");
                return;
            }
        }
        protected void Upload_Click(object sender, EventArgs e)
        {
            Session["IncidentSelected"] = incidentSelect.SelectedItem.Text;
            if (Session["IncidentSelected"] == "Select One") {
                Session["Message"] = "Must select an Incident";
                Response.Redirect("Error.aspx");
                return;
            }
            try
            {
                string webeocData = "<data>";
                StreamReader reader = new StreamReader(FileUploader.FileContent);
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(reader);
                int countMatchUps = 0;
                foreach (XmlNode node in xDoc.DocumentElement)
                {

                    foreach (XmlNode child in node.ChildNodes)
                    {
                        string webeocField = matchField(child.Name);
                        if (webeocField != null)
                        {
                            countMatchUps++;
                            webeocData += "<" + webeocField + ">" + child.InnerText + "</" + webeocField + ">";
                        }

                    }

                }
                webeocData += "</data>";
                if (countMatchUps == 0)
                {
                    Session["Message"] = "Check the File Uploaded, the field names did not match up";
                    Response.Redirect("Error.aspx");
                    return;
                }

                //Save2WebEOC(webeocData);
            }
            catch (Exception)
            {
                Session["Message"] = "Check the File Uploaded, the field names did not match up";
                Response.Redirect("Error.aspx");
                return;
            }
        }
        protected void Save2WebEOC(string data){
            WebEOC.API api = new WebEOC.API();
            api.Url = ConfigurationManager.AppSettings["WebEOCServerURL"];

            int dataid = api.AddData(creds(), ConfigurationManager.AppSettings["WebEOCBoard"], ConfigurationManager.AppSettings["WebEOCBoardInput"], data);
            Session["Message"] = "Data Saved to WebEOC, Data ID=" + dataid.ToString();
            Response.Redirect("Success.aspx");

        }
        protected string matchField(string winlinkFormName)
        {
            
            List<fields> ListOfFields = ListOfFeildsMethod();
            foreach (fields f in ListOfFields) {
                if (winlinkFormName == f.origin) {
                    return f.destination;
                }
            
            }

            return null;
        }
        protected static List<fields> ListOfFeildsMethod()
        {
           
                var reader = new StreamReader(File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FieldMatchUp.csv")));
                List<fields> ListOfFeilds = new List<fields>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    ListOfFeilds.Add(new fields { origin = values[0], destination = values[1] });
                }
                return ListOfFeilds;     
            
        }
        protected WebEOC.WebEOCCredentials creds()
        {
            WebEOC.WebEOCCredentials creds = new WebEOC.WebEOCCredentials();
            creds.Username = ConfigurationManager.AppSettings["WebEOCUser"];
            creds.Password = ConfigurationManager.AppSettings["WebEOCPassword"];
            creds.Position = ConfigurationManager.AppSettings["WebEOCPosition"];
            if (Session["IncidentSelected"] != "" && Session["IncidentSelected"] != "Select One")
            {
                creds.Incident = Session["IncidentSelected"].ToString();
            }

            return creds;
        }
    }
}