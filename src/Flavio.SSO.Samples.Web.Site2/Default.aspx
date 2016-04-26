<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Flavio.SSO.Samples.Web.Site2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7" crossorigin="anonymous">
</head>
<body>
	<form id="form1" runat="server">
		<div class="container">
			<div class="jumbotron">
				<h1>Hello
					<asp:Label ID="lblName" runat="server"></asp:Label>
					! You're authenticated!</h1>
				<p class="lead">Your groups:
					<asp:Label ID="lblGroups" runat="server"></asp:Label></p>
			</div>
			<p><a href="http://localhost:52346/Logout">Logout</a></p>
		</div>
	</form>
</body>
</html>
