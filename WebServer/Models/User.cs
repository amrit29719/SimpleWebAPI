using System.Collections.Generic;

namespace WebServer.Models{
    public class User{
        public int ID{get; set;}
        public string Name{get; set;}
        public string Address{get; set;}
        public long Phone{get; set;}

    }

    public class SampleData{
        public static IDictionary<int, User> Users; 

        static SampleData(){
            Users = new Dictionary<int, User>();
            Users.Add(0, new User{ID = 0, Name = "Alice", Address = "Wonderland", Phone = 6667778888});
            Users.Add(1, new User{ID = 1, Name = "Bob", Address = "La La Land", Phone = 7778889999});
            Users.Add(2, new User{ID = 2, Name = "Charlie", Address = "Disneyland", Phone = 8889990000});

        }

    }

     // used to display status messages in json format
    public class Message{
        public string result{get; set;}
       
    }

    // Patch request uses this class to demonstrate only Address and Phone properties are modifiable 
    public class UserPatchRequest
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
    }
}
