namespace Inventory.Product.API.Extensions;

[AttributeUsage(AttributeTargets.Class,Inherited = false)]
public class BSonCollectionAttribute:Attribute
{
    
    public string CollectionName { get; }
    public BSonCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}