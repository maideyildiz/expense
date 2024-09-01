using Google.Cloud.Firestore;
using ExpenseTracker.Core.Models;
namespace ExpenseTracker.Infrastructure.Services;

public class BaseService<T> : IBaseService<T> where T : Base
{
    private readonly FirestoreDb _db;
    private readonly string _collectionName;

    public BaseService(string collectionName)
    {
        string pathToCredentials = "path/to/your/credentials.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", pathToCredentials);
        _db = FirestoreDb.Create("your-project-id");
        _collectionName = collectionName;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        CollectionReference collection = _db.Collection(_collectionName);
        QuerySnapshot snapshot = await collection.GetSnapshotAsync();
        return snapshot.Documents.Select(doc => doc.ConvertTo<T>());
    }

    public async Task<T> GetByIdAsync(int id)
    {
        DocumentReference docRef = _db.Collection(_collectionName).Document(id.ToString());
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ConvertTo<T>() : null;
    }

    public async Task AddAsync(T obj)
    {
        DocumentReference docRef = _db.Collection(_collectionName).Document(obj.Id.ToString());
        await docRef.SetAsync(obj);
    }

    public async Task UpdateAsync(T obj)
    {
        DocumentReference docRef = _db.Collection(_collectionName).Document(obj.Id.ToString());
        await docRef.SetAsync(obj, SetOptions.MergeAll);
    }

    public async Task DeleteAsync(int id)
    {
        DocumentReference docRef = _db.Collection(_collectionName).Document(id.ToString());
        await docRef.DeleteAsync();
    }
}

