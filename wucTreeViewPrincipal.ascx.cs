using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

partial class wucTreeViewPrincipal : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    public void PopulateNode(Object sender, TreeNodeEventArgs e)
    {

        switch (e.Node.Depth)
        {
            case 0:
                PopulateEmpresa(e.Node);
                break;
            default:
                // Do nothing.
                break;
        }

    }


    void PopulateEmpresa(TreeNode node)
    {
        DataSet ResultSet = RunQuery("Select EmpresaID, Nome From Empresas");

        if (ResultSet.Tables.Count > 0)
        {
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["EmpresaId"].ToString());

                //NewNode.PopulateOnDemand = true;

                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                NewNode.NavigateUrl = "Bus.aspx?id=" + NewNode.Value;

                node.ChildNodes.Add(NewNode);

                PopulateBU(NewNode, row["EmpresaId"].ToString()); //
            }

        }

    }

    void PopulateBU(TreeNode node, string IdPai)
    {

        DataSet ResultSet = RunQuery("Select BUId, Nome From BUs WHERE EmpresaId = " + IdPai + " ORDER BY Nome");

        if (ResultSet.Tables.Count > 0)
        {
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["BUId"].ToString());

                //NewNode.PopulateOnDemand = true;

                NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                NewNode.NavigateUrl = "Departamentos.aspx?id=" + NewNode.Value;

                node.ChildNodes.Add(NewNode);

                PopulateDepartamento(NewNode, row["BUId"].ToString());

            }

        }

    }

    void PopulateDepartamento(TreeNode node, string IdPai)
    {
        DataSet ResultSet = RunQuery("Select DepartamentoId, Nome From Departamentos WHERE BUId = " + IdPai + " ORDER BY Nome");

        if (ResultSet.Tables.Count > 0)
        {
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["DepartamentoId"].ToString());

                //NewNode.PopulateOnDemand = false;

                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                NewNode.NavigateUrl = "Setores.aspx?id=" + NewNode.Value;

                node.ChildNodes.Add(NewNode);

                PopulateSetor(NewNode, row["DepartamentoId"].ToString());
            }
        }
    }

    void PopulateSetor(TreeNode node, string IdPai)
    {
        DataSet ResultSet = RunQuery("Select SetorId, Nome From Setores WHERE DepartamentoId = " + IdPai + " ORDER BY Nome");

        if (ResultSet.Tables.Count > 0)
        {
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["SetorId"].ToString());

                //NewNode.PopulateOnDemand = false;

                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                NewNode.NavigateUrl = "Areas.aspx?id=" + NewNode.Value;

                node.ChildNodes.Add(NewNode);

                PopulateArea(NewNode, row["SetorId"].ToString());
            }
        }
    }

    void PopulateArea(TreeNode node, string IdPai)
    {
        DataSet ResultSet = RunQuery("Select AreaId, Nome From Areas WHERE SetorId = " + IdPai + " ORDER BY Nome");

        if (ResultSet.Tables.Count > 0)
        {
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["AreaId"].ToString());

                //NewNode.PopulateOnDemand = false;

                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                NewNode.NavigateUrl = "Processos.aspx?id=" + NewNode.Value;

                node.ChildNodes.Add(NewNode);

                PopulateProcesso(NewNode, row["AreaId"].ToString());

            }

        }

    }

    void PopulateProcesso(TreeNode node, string IdPai)
    {
        DataSet ResultSet = RunQuery("Select ProcessoId, Nome From Processos WHERE AreaId = " + IdPai + " ORDER BY Nome");

        if (ResultSet.Tables.Count > 0)
        {
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {

                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["ProcessoId"].ToString());

                //NewNode.PopulateOnDemand = false;

                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                NewNode.NavigateUrl = "Subprocessos.aspx?id=" + NewNode.Value;

                node.ChildNodes.Add(NewNode);

                PopulateSubprocesso(NewNode, row["ProcessoId"].ToString());
            }
        }
    }

    void PopulateSubprocesso(TreeNode node, string IdPai)
    {
        DataSet ResultSet = RunQuery("Select SubprocessoId, Nome From Subprocessos WHERE ProcessoId = " + IdPai + " ORDER BY Nome");

        if (ResultSet.Tables.Count > 0)
        {
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {
                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["SubprocessoId"].ToString());

                //NewNode.PopulateOnDemand = false;

                NewNode.SelectAction = TreeNodeSelectAction.Expand;

                node.ChildNodes.Add(NewNode);
                NewNode.NavigateUrl = "Atividades.aspx?id=" + NewNode.Value;

                PopulateAtividade(NewNode, row["SubprocessoId"].ToString());
            }
        }
    }

    void PopulateAtividade(TreeNode node, string IdPai)
    {

        // Query for the product categories. These are the values
        // for the second-level nodes.
        DataSet ResultSet = RunQuery("Select AtividadeId, Nome From Atividades WHERE SubprocessoId = " + IdPai + " ORDER BY Nome");

        // Create the second-level nodes.
        if (ResultSet.Tables.Count > 0)
        {

            // Iterate through and create a new node for each row in the query results.
            // Notice that the query results are stored in the table of the DataSet.
            foreach (DataRow row in ResultSet.Tables[0].Rows)
            {

                // Create the new node. Notice that the CategoryId is stored in the Value property 
                // of the node. This will make querying for items in a specific category easier when
                // the third-level nodes are created. 
                TreeNode NewNode = new TreeNode(row["Nome"].ToString(), row["AtividadeId"].ToString());

                // Set the PopulateOnDemand property to true so that the child nodes can be 
                // dynamically populated.
                //NewNode.PopulateOnDemand = false;

                // Set additional properties for the node.
                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                NewNode.NavigateUrl = "Etapas.aspx?id=" + NewNode.Value;

                // Add the new node to the ChildNodes collection of the parent node.
                node.ChildNodes.Add(NewNode);

            }

        }

    }

    DataSet RunQuery(String QueryString)
    {

        // Declare the connection string. This example uses Microsoft SQL Server and connects to the
        // Northwind sample database.
        String ConnectionString = ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString.ToString();

        SqlConnection DBConnection = new SqlConnection(ConnectionString);
        SqlDataAdapter DBAdapter;
        DataSet ResultsDataSet = new DataSet();

        try
        {

            // Run the query and create a DataSet.
            DBAdapter = new SqlDataAdapter(QueryString, DBConnection);
            DBAdapter.Fill(ResultsDataSet);

            // Close the database connection.
            DBConnection.Close();

        }
        catch (Exception ex)
        {

            // Close the database connection if it is still open.
            if (DBConnection.State == ConnectionState.Open)
            {
                DBConnection.Close();
            }
        }

        return ResultsDataSet;

    }


    void fill_Tree()
    {
        /*
        * Fill the treeview control Root Nodes From Parent Table
        * and child nodes from ChildTables
        */

        /*
        * Create an SQL Connection to connect to you our database
        */

        SqlConnection SqlCon = new SqlConnection("server=D_hameed;uid=sa;pwd=airforce;database=test");

        SqlCon.Open();

        /*
        * Query the database
        */

        SqlCommand SqlCmd = new SqlCommand("Select * from ParentTable", SqlCon);

        /*
        *Define and Populate the SQL DataReader
        */

        SqlDataReader Sdr = SqlCmd.ExecuteReader();

        /*
        * Dispose the SQL Command to release resources
        */

        SqlCmd.Dispose();

        /*
        * Initialize the string ParentNode.
        * We are going to populate this string array with our            ParentTable Records
        * and then we will use this string array to populate our TreeView1 Control with parent records
        */

        string[,] ParentNode = new string[100, 2];

        /*
        * Initialize an int variable from string array index
        */

        int count = 0;

        /*
        * Now populate the string array using our SQL Datareader Sdr.

        * Please Correct Code Formatting if you are pasting this code in your application.
        */

        while (Sdr.Read())
        {

            ParentNode[count, 0] = Sdr.GetValue(Sdr.GetOrdinal("ParentID")).ToString();
            ParentNode[count++, 1] = Sdr.GetValue(Sdr.GetOrdinal("ParentName")).ToString();

        }

        /*
        * Close the SQL datareader to release resources
        */

        Sdr.Close();

        /*
        * Now once the array is filled with [Parentid,ParentName]
        * start a loop to find its child module.
        * We will use the same [count] variable to loop through ChildTable
        * to find out the number of child associated with ParentTable.
        */

        for (int loop = 0; loop < count; loop++)
        {

            /*
            * First create a TreeView1 node with ParentName and than
            * add ChildName to that node
            */

            TreeNode root = new TreeNode();
            root.Text = ParentNode[loop, 1];
            root.Target = "_blank";

            /*
            * Give the url of your page
            */

            root.NavigateUrl = "mypage.aspx";

            /*
            * Now that we have [ParentId] in our array we can find out child modules

            * Please Correct Code Formatting if you are pasting this code in your application.

            */

            SqlCommand Module_SqlCmd = new SqlCommand("Select * from ChildTable where ParentId =" + ParentNode[loop, 0], SqlCon);

            SqlDataReader Module_Sdr = Module_SqlCmd.ExecuteReader();

            while (Module_Sdr.Read())
            {

                // Add children module to the root node

                TreeNode child = new TreeNode();

                child.Text = Module_Sdr.GetValue(Module_Sdr.GetOrdinal("ChildName")).ToString();

                child.Target = "_blank";

                child.NavigateUrl = "your_page_Url.aspx";

                root.ChildNodes.Add(child);

            }

            Module_Sdr.Close();

            // Add root node to TreeView
            TreeView1.Nodes.Add(root);

        }

        /*
        * By Default, when you populate TreeView Control programmatically, it expends all nodes.
        */
        TreeView1.CollapseAll();
        SqlCon.Close();

    }



}