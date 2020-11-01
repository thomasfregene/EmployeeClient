using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeClient
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetEmployee_Click(object sender, EventArgs e)
        {
            //instantiating the proxy class
            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");

            //getting the employee object from the service and passing the value to the webform
            EmployeeService.Employee employee = client.GetEmployee(Convert.ToInt32(txtID.Text));
            txtName.Text = employee.Name;
            txtGender.Text = employee.Gender;
            txtDateOfBirth.Text = employee.DateOfBirth.ToShortDateString();

            lblMessage.Text = "Employee retrieved";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //instantiating the proxy class
            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");

            //employee object
            EmployeeService.Employee employee = new EmployeeService.Employee();
            //getting the new data from the webform to insert in employee table in Db using the service
            employee.Id = Convert.ToInt32(txtID.Text);
            employee.Name = txtName.Text;
            employee.Gender = txtGender.Text;
            employee.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);

            client.SaveEmployee(employee);

            lblMessage.Text = "Employee Saved";
        }
    }
}