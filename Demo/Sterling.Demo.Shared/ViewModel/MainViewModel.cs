using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Sterling.Common;

namespace Sterling.Demo.Shared
{

	public class MainViewModel : INotifyPropertyChanged
	{
		private string counter;
		private string message;
	

		private PersonRespository pr;
		private DbRepository<ItemModel,int> ir;
		private DbRepository<Order,Guid> or;
		private DbRepository<Media,long> mr;
		private DbRepository<Login> lr;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<ItemModel> Items { get; private set; }

		public ObservableCollection<PersonModel> People { get; private set; }

		public ObservableCollection<Order> Orders { get; private set; }

		public ObservableCollection<Media> Library { get; private set; }

		public string Counter {
			get {
				return counter;
			}
			set {
				counter = value;
				NotifyPropertyChanged ("Counter");
			}
		}

		public string Message {
			get {
				return message;
			}
			set {
				message = value;
				NotifyPropertyChanged ("Message");
			}
		}

		public MainViewModel ()
		{
			AppDb.Init ();
			this.Items = new ObservableCollection<ItemModel> ();
			People = new ObservableCollection<PersonModel> ();
			Orders = new ObservableCollection<Order> ();
			Library = new ObservableCollection<Media> ();
			pr = new PersonRespository ();
			ir = new DbRepository<ItemModel,int> (AppDb.Database);
			or = new DbRepository<Order,Guid> (AppDb.Database);
			mr = new DbRepository<Media, long> (AppDb.Database);
			lr = new DbRepository<Login> (AppDb.Database);
		}

		public bool DataExists ()
		{
			return AppDb.Database.Query<PersonModel, int> ().Any ();
		}

		public async void LoadData ()
		{
			try {
				bool hasKeys = await pr.HasKey ();

				if (!hasKeys) {
					Console.WriteLine ("Database has not been initialized");
					await _SetupData ();
					Message = "Database was created and populated";
				} else {
					Message = "Database already exists";
				}

//                var items = await ir.GetAll();
//                foreach (var item in items)
//                {
//                    Items.Add(item);
//                }
//
//				var logins = await lr.GetAll();
//				var lg = logins.FirstOrDefault();
//				var lgg = await lr.GetById("lbrown");
//
//                var list = await pr.GetAll();
//                foreach (var person in list)
//                {
//                    People.Add(person);
//
//                }

				//Test select by Id
				//var obj = await pr.GetById(4);
				//Test custom select contains
				//var obj1 = await pr.GetNameContains("Homes");

				//var obj2 = await pr.GetNameContains("Seth");

				//Test getting collection with GUID as primary key
				//var orderList = await or.GetAll();

				//Test getting collection with Long as primary key
				//var mediaList = await mr.GetAll();


				var testResult = await StressTest ();
				Console.WriteLine ("The resultset is " + testResult);
				Message = BuildMessage (1, testResult);

				var testResult1 = await StressTest ();
				Console.WriteLine ("The resultset is " + testResult1);
				Message = BuildMessage (2, testResult1);
      
			} catch (Exception ex) {

				var m = ex.Message;
 
			}

		}

		private string BuildMessage (int testIndex, int num)
		{
			var result = num == 3 ? "Success" : "Failed";
			return string.Format ("Test {0} performed 2001 queries, 3000 deletes, 3000 adds: test result {1}", testIndex, result);
		}

		private async Task<bool> _SetupData ()
		{
			var l = new Login (){ UserName = "lbrown", Email = "azdevelop.net@gmail.com" };
			lr.AddOrUpdate (l);

			var personData = new List<PersonModel> () {
				new PersonModel (){ FirstName = "Jack", LastName = "Sparrow", DOB = DateTime.Now, Sex = Gender.Male },
				new PersonModel (){ FirstName = "Seth", LastName = "Knox", DOB = DateTime.Now.AddYears (-15), Sex = Gender.Male },
				new PersonModel () {
					FirstName = "Sam",
					LastName = "Houston",
					DOB = DateTime.Now.AddYears (-4),
					Sex = Gender.Male
				},
				new PersonModel () {
					FirstName = "Jill",
					LastName = "Homes",
					DOB = DateTime.Now.AddYears (-66),
					Sex = Gender.Female
				},
				new PersonModel (){ FirstName = "Jill", LastName = "Beck", DOB = DateTime.Now.AddYears (-66), Sex = Gender.Female },
				new PersonModel () {
					FirstName = "Max",
					LastName = "Williams",
					DOB = DateTime.Now.AddYears (-24),
					Sex = Gender.Male
				},
				new PersonModel (){ FirstName = "Seth", LastName = "Harmon", DOB = DateTime.Now.AddYears (-45), Sex = Gender.Male },
				new PersonModel () {
					FirstName = "Jen",
					LastName = "Barstow",
					DOB = DateTime.Now.AddYears (-30),
					Sex = Gender.Female
				},
				new PersonModel () {
					FirstName = "Beth",
					LastName = "Quincy",
					DOB = DateTime.Now.AddYears (-20),
					Sex = Gender.Female
				},
				new PersonModel () {
					FirstName = "William",
					LastName = "Comes",
					DOB = DateTime.Now.AddYears (-10),
					Sex = Gender.Male
				},
				new PersonModel () {
					FirstName = "Seth",
					LastName = "Stand",
					DOB = DateTime.Now.AddYears (-5),
					Sex = Gender.Male
				}
                
			};
			await pr.SaveCollection (personData);
		
			var sampleData = new List<ItemModel> () {
				new ItemModel () {
					LineOne = "runtime one",
					LineTwo = "Maecenas praesent accumsan bibendum",
					LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu"
				},
				new ItemModel () {
					LineOne = "runtime two",
					LineTwo = "Dictumst eleifend facilisi faucibus",
					LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus"
				},
				new ItemModel () {
					LineOne = "runtime three",
					LineTwo = "Habitant inceptos interdum lobortis",
					LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent"
				},
				new ItemModel () {
					LineOne = "runtime four",
					LineTwo = "Nascetur pharetra placerat pulvinar",
					LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos"
				},
				new ItemModel () {
					LineOne = "runtime five",
					LineTwo = "Maecenas praesent accumsan bibendum",
					LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur"
				},
				new ItemModel () {
					LineOne = "runtime six",
					LineTwo = "Dictumst eleifend facilisi faucibus",
					LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent"
				},
				new ItemModel () {
					LineOne = "runtime seven",
					LineTwo = "Habitant inceptos interdum lobortis",
					LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat"
				},
				new ItemModel () {
					LineOne = "runtime eight",
					LineTwo = "Nascetur pharetra placerat pulvinar",
					LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum"
				},
				new ItemModel () {
					LineOne = "runtime nine",
					LineTwo = "Maecenas praesent accumsan bibendum",
					LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu"
				},
				new ItemModel () {
					LineOne = "runtime ten",
					LineTwo = "Dictumst eleifend facilisi faucibus",
					LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus"
				},
				new ItemModel () {
					LineOne = "runtime eleven",
					LineTwo = "Habitant inceptos interdum lobortis",
					LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent"
				},
				new ItemModel () {
					LineOne = "runtime twelve",
					LineTwo = "Nascetur pharetra placerat pulvinar",
					LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos"
				},
				new ItemModel () {
					LineOne = "runtime thirteen",
					LineTwo = "Maecenas praesent accumsan bibendum",
					LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur"
				},
				new ItemModel () {
					LineOne = "runtime fourteen",
					LineTwo = "Dictumst eleifend facilisi faucibus",
					LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent"
				},
				new ItemModel () {
					LineOne = "runtime fifteen",
					LineTwo = "Habitant inceptos interdum lobortis",
					LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat"
				},
				new ItemModel () {
					LineOne = "runtime sixteen",
					LineTwo = "Nascetur pharetra placerat pulvinar",
					LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum"
				}
			};
			await ir.SaveCollection (sampleData);

			var orderDat = new List<Order> () {
				new Order (){ Description = "Cars", TimeStamp = DateTime.Now },
				new Order (){ Description = "Planes", TimeStamp = DateTime.Now },
				new Order (){ Description = "Boats", TimeStamp = DateTime.Now },
				new Order (){ Description = "Trains", TimeStamp = DateTime.Now },
			};
			await or.SaveCollection (orderDat);

			var mediaDat = new List<Media> () {
				new Media (){ Description = "Records" },
				new Media (){ Description = "Magazines" },
				new Media (){ Description = "Books" },
				new Media (){ Description = "DVDs" },
			};
			await mr.SaveCollection (mediaDat);

			return true;
		}

		public async Task<int> StressTest ()
		{
			for (int x = 0; x < 1000; x++) {
				Counter = "Iteration " + x + " in process";

				var list = await pr.GetNameContains ("Seth");
				foreach (var obj in list) {
					await pr.Delete (obj);
		
				}
				var newList = await pr.GetNameContains ("Seth");
				if (newList.Count == 0) {
					foreach (var obj in list) {
						await pr.AddOrUpdate (obj);
					}
				}
			}
			var finalList = await pr.GetNameContains ("Seth");
			return finalList.Count;
		}

		private void NotifyPropertyChanged (String propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (null != handler) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}