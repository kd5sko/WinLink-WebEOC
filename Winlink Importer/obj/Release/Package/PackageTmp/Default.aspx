<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Winlink_Importer._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
   
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
 <link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-blue.css">
       <title>WinLink Importer</title>
</head>
<body>
    <form id="form1" runat="server">
  <div>
       <div class="container-fluid">
             <nav class="navbar navbar-red w3-container w3-theme w3-card-2">
    <div class="navbar-header">
           <div class="">
    <h1><span id="EventName" runat="server"></span>Winlink Request Importer</h1>

               </div>
           </div></nav>
                
    
      
      <div ID="serverInfo" runat="server">

      </div>
        <div>Attach the Request xsl</div>
        <asp:FileUpload ID="FileUploader" runat="server" />
   
        <asp:Button ID="Upload" runat="server" Text="Upload" OnClick="Upload_Click" />
     </div> </div>


    </form>
</body>
</html>
