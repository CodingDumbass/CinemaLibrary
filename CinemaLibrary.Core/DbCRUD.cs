using CinemaLibrary.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CinemaLibrary.Core
{
    public class DbCRUD<T> where T : class
    {
        private string Default_create_file_name = $"{nameof(T)}CreateUsersTemplate{FileFinder.GetFileId()}.txt";
        private FileFinder File_finder;
        private CinemaLibraryContext db_context;
        private GenericValueConverter generic_converter;
        public DbCRUD()
        {
            db_context = new CinemaLibraryContext();
        }
        public void CreateUsers(string? file_name = null)
        {
            File_finder = new FileFinder(file_name == null? Default_create_file_name : file_name);

            string path = File_finder.FindFilePath();

            var contents = File_finder.GetFileContents(path).Select(x => x.Split(' ').ToList()).ToList();

            var namespace_name = MethodBase.GetCurrentMethod().DeclaringType.Namespace;

            var type = Type.GetType($"{namespace_name}.{ConvertIdentityName()}");

            var instance = Activator.CreateInstance(type);

            generic_converter = new GenericValueConverter();

            foreach(var line in contents)
            {
                foreach (var property in instance.GetType().GetProperties())
                {
                    var genericmethodInfo = typeof(GenericValueConverter).GetMethod(nameof(generic_converter.FindMatch)).MakeGenericMethod(property.PropertyType);

                    var result = genericmethodInfo.Invoke(generic_converter, new object[] {line});

                    property.SetValue(instance, result);
                }
                db_context.Add(instance);

                db_context.SaveChanges();
            }
        }
        private string ConvertIdentityName()
        {
            var model_name = nameof(T);

            var exceptions = new List<string> {"Categories", "Favourites", "Movies" };

            if (exceptions.Contains(model_name))
                return model_name;
            else
                return $"AspNet{model_name.Remove(0, 8)}";
                
        }
    }
}
