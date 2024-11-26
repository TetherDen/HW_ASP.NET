namespace HW_05.Models
{
    public static class HtmlPageGenerator
    {

        public static string GenerateHtmlPage(string title, string bodyContent)
        {
            return $@"
                <html>
                    <head>
                        <title>{title}</title>
                    </head>
                    <body>
                        <h1>{title}</h1>
                        {bodyContent}
                    </body>
                </html>";
        }


        public static string GenerateLoginPage()
        {
            string loginForm = @"
                <form method='POST' action='/login'>
                    <label for='Email'>Email:</label><br>
                    <input type='text' id='Email' name='Email' required><br><br>
                
                    <label for='Password'>Password:</label><br>
                    <input type='password' id='Password' name='Password' required><br><br>
                
                    <input type='submit' value='Login'>
                </form>
                <p>Don't have an account? <a href='/register'>Register here</a></p>";

            return GenerateHtmlPage("Login", loginForm);
        }


        public static string GenerateRegisterPage()
        {
            string registerForm = @"
                <form method='POST' action='/register'>
                    <label for='Name'>Name:</label><br>
                    <input type='text' id='Name' name='Name' required><br><br>
                
                    <label for='Email'>Email:</label><br>
                    <input type='text' id='Email' name='Email' required><br><br>
                
                    <label for='Password'>Password:</label><br>
                    <input type='password' id='Password' name='Password' required><br><br>
                
                    <input type='submit' value='Register'>
                </form>
                <p>Already have an account? <a href='/'>Login here</a></p>";

            return GenerateHtmlPage("Registration", registerForm);
        }


        public static string GenerateNewServiceForm(string token)
        {
            string newServiceForm = $@"
                <form method='POST' action='/newService?token={token}'>
                    <label for='Name'>Service Name:</label><br>
                    <input type='text' id='Name' name='Name' required><br><br>

                    <label for='PhoneNumber'>Phone Number:</label><br>
                    <input type='text' id='PhoneNumber' name='PhoneNumber' required><br><br>

                    <label for='Description'>Description:</label><br>
                    <textarea id='Description' name='Description' required></textarea><br><br>

                    <input type='submit' value='Register Service'>
                </form>";

            return GenerateHtmlPage("New Service Registration", newServiceForm);
        }


        public static string GenerateMyServicesPage(List<CustomerService> services, string token)
        {
            string rows = "";

            foreach (var service in services)
            {
                rows += $@"
                    <tr>
                        <td>{service.Name}</td>
                        <td>{service.PhoneNumber}</td>
                        <td>{service.Description}</td>
                        <td>{service.RegistrationDate}</td>
                    </tr>";
            }

            string table = $@"
            <h2>Your Services</h2>
            <table border='1'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Phone Number</th>
                        <th>Description</th>
                        <th>Registration Date</th>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
            <br>
            <a href='/newService?token={token}'>Add New Service</a>";

            return GenerateHtmlPage("My Services", table);
        }

    }



}
