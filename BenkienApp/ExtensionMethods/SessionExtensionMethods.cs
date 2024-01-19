using Newtonsoft.Json;

namespace BenkienApp.ExtensionMethods
{
    public static class SessionExtensionMethods
    {
        public static void SetJson(this ISession session, string key, object value) // dışarıdan object (hertürlü nesne) türünde değer aldık
        {
            string objectString = JsonConvert.SerializeObject(value); // string bir değişken oluşturup parametreden gelen value yi json a çevirip değişkene atadık
            session.SetString(key, objectString); // session a json nesnesini string olarak yükledik
        }
        public static T GetJson<T>(this ISession session, string key) where T : class // sessiondaki veriyi t ( yani generic olarak) tutan ve bize kullanmak istediğimiz yerde getirecek metodumuz
        {
            string objectString = session.GetString(key); // sessiondaki veriyi string değişkenimize aktardık
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            T value = JsonConvert.DeserializeObject<T>(objectString); // json verimizi tekrardan nesneye çevirip çağırıldığı yere gönderdik.
            return value;
        }
        //json ayarlamak 
        //jsondaki veriyi çekmek
    }
}