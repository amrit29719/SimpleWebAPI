using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;


namespace WebServer.Controllers{

    [Route("api/[controller]")]
    public class UsersController: Controller{
        
        [HttpGet]
        public ActionResult Get() {
            Message m = new Message();
            //returns all user data if any available 
            if (SampleData.Users.Count != 0) {
                return Ok(SampleData.Users.Values.ToArray());
            }
            m.result = "No content available";
            return NotFound(m);
            
        }

        [HttpGet("{id}")]
        public ActionResult GetUserByID(int id){
            // returns user data for user ID in url
            if(SampleData.Users.ContainsKey(id)){
                return Ok(SampleData.Users[id]);
            }
            else{
                Message m = new Message();
                m.result = "Requested key does not exist";
                return NotFound(m);
            }
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody]User user){
            Message m = new Message();
            try{
            user.ID = SampleData.Users.Keys.Max() + 1;
            SampleData.Users.Add(user.ID, user);
            return Created($"api/users/{user.ID}", user);           // contains new ID
            }
            catch{
                m.result = "invalid request";
                return BadRequest(m);                // invalid data requested for post
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUserByID(int id, [FromBody]User user){
         
            Message m = new Message();

            try{
                // replaces data for user ID given in url  
                if(SampleData.Users.ContainsKey(id)){
                    // only replaces data for user if all attributes other than ID are changed
                    if(user.Name == null || user.Address == null || user.Phone == 0){
                        m.result = "invalid request : missing parameters";
                        return BadRequest(m);
                    }
                    
                    // status 400 returned if id is different in url and body
                    if(id != user.ID){
                        m.result = "You are not allowed to change ID";
                        return BadRequest(m);
                    }

                    // replaces the user with one requested
                    SampleData.Users[id] = user;
                    m.result = "Update Successful";
                    return Ok(m);
                    
                }
                // not found returned if url doesn't exist
                m.result = "Requested user ID does not exist";
                return NotFound(m);
            }
            catch{
                m.result = "invalid request";
                return BadRequest(m);
            }
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteUserByID(int id) {
            Message m = new Message();
            try{
                if (SampleData.Users.ContainsKey(id)) {
                    SampleData.Users.Remove(id);
                    m.result = "Successfully deleted";
                    return Ok(m);
                }

                m.result = "Requested user ID does not exist";
                return NotFound(m);
            }
            catch{
                m.result = "invalid request";
                return BadRequest(m);
            }
        }

        [HttpPatch]
        public ActionResult PatchUser([FromBody]UserPatchRequest request){
            Message m = new Message();
            try{
                if(SampleData.Users[request.ID] == null){
                    m.result = "Requested user ID does not exist";
                    return NotFound(m);
                }
                else{
                    // updates only address if address attribute provided in body
                    if(request.Address != null){
                        SampleData.Users[request.ID].Address = request.Address;
                    }
                    // updates only phone if phone attribite provided in body
                    if(request.Phone != 0){
                        SampleData.Users[request.ID].Phone = request.Phone;
                    }
                    m.result = "Patch Update Successful";
                    return Ok(m);
                }


            }
            catch{
                m.result = "invalid request";
                return BadRequest(m);
            }
        }

       


    }
        
   
}
