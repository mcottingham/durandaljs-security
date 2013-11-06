using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DurandalSecurity.Models.SaveValidators.Validators;

namespace DurandalSecurity.Models.SaveValidators {
    public class SaveValidatorFactory<T> where T : class {
        private readonly Dictionary<string, Type> _foundTypes;
        private readonly Assembly[] _assemblies;

        public SaveValidatorFactory() : this(Assembly.GetExecutingAssembly()) {
        }

        public SaveValidatorFactory(params Assembly[] assemblies)
            : this(StringComparer.OrdinalIgnoreCase, assemblies) {
        }

        public SaveValidatorFactory(IEqualityComparer<string> comparer, params Assembly[] assemblies) {
            _assemblies = assemblies;
            _foundTypes = assemblies.SelectMany(a => a.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface))
                                    .ToDictionary(t => t.Name, comparer);
        }

        public T CreateInstance(string name, params object[] args) {
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentNullException("name");
            }

            Type type;
            var objectName = String.Format("{0}Validator", name);
            if (!_foundTypes.TryGetValue(objectName, out type)) {
                if (!_foundTypes.TryGetValue(typeof(GenericValidator<>).Name, out type)) {
                    throw new ArgumentException("Failed to find type named '" + objectName + "' in ObjectFactory of type '" + typeof(T).Name + "'.");
                }
                else {
                    var genericType = typeof(GenericValidator<>);                    
                    var entityType = GetType(name);
                    var genericTypeArgs = new Type[] { entityType };
                    return (T)Activator.CreateInstance(genericType.MakeGenericType(genericTypeArgs), args);
                }
            }
            else {
                return (T)Activator.CreateInstance(type, args);
            }
        }

        private Type GetType(string objectName) {
            return _assemblies.Select(t => t.GetTypes().Where(type => type.Name == objectName)).FirstOrDefault().FirstOrDefault();
        }
    }
}