using Google.Cloud.Firestore;
using Xunit;
using System.Threading.Tasks;

namespace FirestoreTests
{
    public class FirestoreTests
    {
        private FirestoreDb _db;

        public FirestoreTests()
        {
            _db = FirestoreDb.Create("your-project-id");
        }

        [Fact]
        public async Task TestConnection()
        {
            // Basit bir koleksiyon ve belge oluşturun
            CollectionReference collection = _db.Collection("test-collection");
            DocumentReference docRef = collection.Document("test-document");

            // Veri ekleyin
            await docRef.SetAsync(new { message = "Firestore is connected!" });

            // Veriyi okuyun
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            Assert.True(snapshot.Exists, "Document should exist.");
            Assert.Equal("Firestore is connected!", snapshot.GetValue<string>("message"), "Document data should match.");
        }
    }
}
