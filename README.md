Mono SterlingDB
===============

###Overview

On a recent project we experienced some issue using SQLite. Problems included thread cancellations and locked files when the database was being written and read in at once in high volumne. There are lots of articles talking about how to resolve such issues but I wanted to see if there was an alternative since I had used NoSql databases before.

I found out on codeplex a great project by Jeremy Likeness with source using IsolatedStorage in Windows Phone.  I decided that this would be a great project to migrate into Mono for iOS and Android. I tested this solution in an existing project that experienced issues with SQLite and it worked like a dream.

In the Sterling_IOS_UnitTestViewController of IOS and MainActivity of Android test projects there are additional comments describing how to deploy to release with the setting of link all assemblies.  By the way, this setting reduces and optimizes your application significantly.

Please give Jeremy the credit and observe the Microsoft Public License (Ms-PL). Source is here with examples so you can make any modifications needed to suit your needs. The original project can be found at:

http://sterling.codeplex.com/

Additional documentation from Jeremy can be found at:

https://sites.google.com/site/sterlingdatabase/sterling-user-guide/getting-started

###Code Example:

You essentially need two files to get SterlingDb going.  One file defines the triggers, creates the tables,defines primary keys and inherits from the BaseDatabaseInstance class. The other class initializes the database engine, logging and an instance of your database class mentioned before. The following code example defines a database based on a person model.

      public class AppDatabase : BaseDatabaseInstance {
	//This class is only needed if you want to explicitly define the primary key yourself. 
        //Otherwise there are helper classes that can do this
        public class PersonTrigger : BaseSterlingTrigger<PersonModel , int>
        {
            private int _nextId;

            public PersonTrigger(int nextId)
            {
                _nextId = nextId;
            }

            public override bool BeforeSave(PersonModel  instance)
            {

                if (instance.Id < 1)
                {
                    instance.Id = _nextId++;
                }

                return true;
            }

            public override void AfterSave(PersonModel  instance)
            {
                return;
            }

            public override bool BeforeDelete(int key)
            {
                return true;
            }
        }

        public override string Name
        {
            get { return "AppDatabase"; }
        }

        protected override System.Collections.Generic.List<ITableDefinition> RegisterTables()
        {
            var list = new System.Collections.Generic.List<ITableDefinition>();
            list.Add(CreateTableDefinition<PersonModel ,int>(i => i.Id));
            return list;
        
        }
    }
    
    public class AppDb
    {
        private static ISterlingDatabaseInstance _database = null;
        private static SterlingEngine _engine = null;
        private static SterlingDefaultLogger _logger = null;

        public static ISterlingDatabaseInstance Database
        {
            get
            {
                return _database;
            }
        }

        public static void Init(){
            if(_database==null)
                _ActivateEngine();
        }

        public static void Close(){
            _DeactivateEngine();
        }

        private static void _ActivateEngine()
        {
            _engine = new SterlingEngine();
            _logger = new SterlingDefaultLogger(SterlingLogLevel.Information);
            _engine.Activate();
            _database = _engine.SterlingDatabase.RegisterDatabase<AppDatabase>(new IsolatedStorageDriver());

             var maxPdx =
                _database.Query<PersonModel, int>().Any() ?
            (from id in _database.Query<PersonModel, int>()
                select id.Key).Max() + 1 : 1;

            //This is another way to get the same value from an extension class
	    //var maxPdx = _database.GetIntegerIndex<PersonModel>(); 

            //Register using the custom class you defined earlier
            _database.RegisterTrigger<PersonModel, int>(new AppDatabase.PersonTrigger(maxPdx));

	    //OR - you can register without defining a custom class using a pre-Built template trigger 
	    //passing in the field that is the primary key.
            //_database.RegisterTrigger<PersonModel, int>(new IntTrigger<PersonModel>(maxPdx,"Id"));


        }

        private static void _DeactivateEngine()
        {
            _logger.Detach();
            _engine.Dispose();
            _database = null;
            _engine = null;
        }
    }
    
###Query Examples

Here is code to retrieve all people:

        public async Task<List<PersonModel>> GetAllPeoeple()
        {
            return await Task.Run(() =>
                {
                    var list = new List<PersonModel>();
                    foreach (var obj in AppDb.Database.Query<PersonModel, int>())
                        list.Add(obj.LazyValue.Value);
                    return list;
                });

        }
        
Here is code to search based on a parameter:

        public async Task<PersonModel> GetPersonById(int id)
        {
            return await Task.Run(() =>
                {
                    var person = (from x in AppDb.Database.Query<PersonModel, int>()
                                                 where  x.LazyValue.Value.Id == id
                        select x).FirstOrDefault(); 
                    return person.LazyValue.Value;
                });
                                   
        }
