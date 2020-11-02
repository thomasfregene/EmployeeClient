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

            //for dynamic rendering of form
            if (employee.Type == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                txtAnnualSalary.Text = ((EmployeeService.FullTimeEmployee)employee).AnnualSalary.ToString();
                trAnnualSalary.Visible = true;
                trHourlyRate.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                txtHourlyRate.Text = ((EmployeeService.PartTimeEmployee)employee).HourlyPay.ToString();
                txtHoursWorked.Text = ((EmployeeService.PartTimeEmployee)employee).HoursWorked.ToString();
                trAnnualSalary.Visible = false;
                trHourlyRate.Visible = true;
                trHoursWorked.Visible = true;
            }
            dllEmployeeType.SelectedValue = ((int)employee.Type).ToString();

            txtName.Text = employee.Name;
            txtGender.Text = employee.Gender;
            txtDateOfBirth.Text = employee.DateOfBirth.ToShortDateString();

            lblMessage.Text = "Employee retrieved";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //instantiating the proxy class
            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");

            //setting employee object to null due to employee types
            EmployeeService.Employee employee = new EmployeeService.Employee();


            //getting the new data from the webform to insert in employee table in Db using the service
            if (((EmployeeService.EmployeeType)Convert.ToInt32(dllEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                employee = new EmployeeService.FullTimeEmployee
                {
                    ID = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = EmployeeService.EmployeeType.FullTimeEmployee,
                    AnnualSalary = Convert.ToInt32(txtAnnualSalary.Text)
                };
                client.SaveEmployee(employee);
                lblMessage.Text = "Employee Saved";
            }
            else if (((EmployeeService.EmployeeType)Convert.ToInt32(dllEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.PartTimeEmployee)
            {
                employee = new EmployeeService.PartTimeEmployee
                {
                    ID = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = EmployeeService.EmployeeType.PartTimeEmployee,
                    HourlyPay = Convert.ToInt32(txtHourlyRate.Text),
                    HoursWorked = Convert.ToInt32(txtHoursWorked.Text)
                };
                client.SaveEmployee(employee);
                lblMessage.Text = "Employee Saved";
            }
            else
            {
                lblMessage.Text = "Please select Employee";
            }
            
        }

        protected void dllEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dllEmployeeType.SelectedValue == "-1")
            {
                trAnnualSalary.Visible = false;
                trHourlyRate.Visible = false;
                trHoursWorked.Visible = false;
            }
            else if (dllEmployeeType.SelectedValue == "1")
            {
                trAnnualSalary.Visible = true;
                trHourlyRate.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                trAnnualSalary.Visible = false;
                trHourlyRate.Visible = true;
                trHoursWorked.Visible = true;
            }
        }
    }
}