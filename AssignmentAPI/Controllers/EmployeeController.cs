using common.Models;
using DAL.DAL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeDAL _employeeDAL;
        public EmployeeController()
        {
            _employeeDAL = new EmployeeDAL();
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<EmployeeModel> Get()
        {
            return _employeeDAL.GetAllEmployees();
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IEnumerable<EmployeeModel> Post([FromBody] EmployeeModel employee)
        {
            return _employeeDAL.AddEmployee(employee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut]
        public IEnumerable<EmployeeModel> Put([FromBody] EmployeeModel employee)
        {
            return _employeeDAL.UpdateEmployee(employee);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete]
        public IEnumerable<EmployeeModel>  Delete(int id)
        {
            return _employeeDAL.DeleteEmployee(id);
        }
    }
}
