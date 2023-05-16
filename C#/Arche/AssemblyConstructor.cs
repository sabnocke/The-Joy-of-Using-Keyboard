using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Arche;

// Doesn't work due to the assembly limitations
public class AssemblyConstructor<T>
where T : class
{
    private readonly Type _obj; 
    private readonly string? _fullName;
    private Delegate? _cstDelegate;
    public string? Name;
    public Delegate? CstDelegate
    {
        get => _cstDelegate;
        private set => _cstDelegate = value;
    }
    
    
    public Type Obj
    {
        get => _obj;
        set => throw new AccessDeniedException("Cannot alter current state.");
    }
    
    public AssemblyConstructor(string accessor = "")
    {
        switch (typeof(T) == typeof(Unbound))
        {
            case true:
                if (accessor != "")
                {
                    string typeName = TypeSeeker(accessor);
                    Name = typeName;
                    _obj = Type.GetType(typeName)!;
                    //_fullName = _obj.FullName;
                }
                else
                {
                    throw new ArgumentException("Accessor not given!");
                }
                break;
            
            case false:
                _obj = typeof(T);
                _fullName = _obj.FullName;
                Name = _fullName;
                break;
        }
        // TODO remove if used outside of Arche 
        throw new AccessDeniedException("Cannot run AssemblyConstructor inside Arche");
    }
    
    private string TypeSeeker(string accessor)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        string? fullName = string.Empty;
        Parallel.ForEach(assemblies, varAssembly =>
        {
            Type[] types = varAssembly.GetTypes();
            foreach (var varType in types)
            {
                MethodInfo? methodInfo = varType.GetMethod(accessor);
                if (methodInfo != null)
                {
                    fullName = varType.FullName;
                }
            }
        });
        return fullName;
    }

    public MemberInfo[] AccessMembers()
    {
        return Obj.GetMembers(BindingFlags.NonPublic | 
                               BindingFlags.Instance |
                               BindingFlags.Public);
    }
    
    public Delegate? Delegation(string methodName)
    {
        if (_fullName == null)
        {
            throw new AccessDeniedException("Name of assembly could not be accessed.");
        }
        MethodInfo? methodInfo = Type 
            .GetType(_fullName, true)?
            .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
        if (methodInfo != null)
        {
            var parType = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
            var returnType = methodInfo.ReturnType; 
            var delegateType = Expression.GetDelegateType(parType.Concat(new []{returnType}).ToArray());
            _cstDelegate = Delegate.CreateDelegate(delegateType, null, methodInfo);
            CstDelegate = _cstDelegate;
            return _cstDelegate;
        }

        throw new NotFoundException($"Could not access method ({methodName}).");
    }

    
}






public class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string? message)
    : base(message) { }

    public NotFoundException(string message, Exception innerException)
    : base(message, innerException) { }
}

public class AccessDeniedException : Exception
{
    public AccessDeniedException() { }
    
    public AccessDeniedException(string? message)
        :base(message) { }

    public AccessDeniedException(string message, Exception innerException)
    :base(message, innerException) { }
}
/// <summary>
/// Is used only in AssemblyConstructor's type in case that class is unknown
/// </summary>
public abstract class Unbound
{
    public abstract void Method();
}