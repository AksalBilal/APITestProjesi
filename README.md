# APITestProjesi
Restsharp, Allure ve Log4Net framework kullanarak birkaç senaryo üzerinden API Test projesi oluşturdum. Test sonuclarının loglanması için log4net, monitoring edilmesi için de allure yapısını kullandım. Bu Proje Yazılmış olan API ların girilen parametrelere göre doğru çıktıyı verip vermediğinin kontrollerinin yapıldığı ve bunların yönetilmesini sağlayan monitoring ve logging yardımıyla çok amaçlı olarak isteğe göre değiştirilebilir bir projedir.
 ## Kullanılan Teknolojiler
- **C#** (Proje de tam olarak istediğim yapıyı kurabilmem için bu dili tercih ettim);
- **Nunit3**(Test Caselerin yazılması ve çıktıların kontrollerinde kullandım);
- **RestSharp** (Case de yer alan API ye istek atılmak için kullandım);
- **Newton.Json** (Json ların serialization-deserialization dönüşüm işlemlerinde kullandım);
- **Allure** (Test Sonuclarının monitorize edilmesinde kullandım);
- **Log4Net** (Hata sonuclarının loglanmasında kullandım);
- **Nunit-Console** (Yazılmış olan test senaryolarını console üzerinden koşturup sonuçlarını takip etmek için kullandım);


## Proje Yapısının Açıklaması
 Proje de alttaki yapıyı kullandım. Her başlığın ne için açıldığını ve ne zaman kullanıldığını altta açıklıyorum.
 
 ![image](https://user-images.githubusercontent.com/46024317/76700854-b8eb2080-66cc-11ea-8a1d-2bc8fa987853.png)
 
  - **Base**: Genel olarak kod içerisinde fazla kullanılacağını düşündüğüm kod yapılarının tekrarını önlemek için tek bir yerden oluşturup yönetimi kolaylaştırdım. Suan icerisinde 'RestHttpClient' ve 'BaseClass' isimli sınıflarimiz var.
     - RestHttpClient: Yapılacak olan http isteklerini tek bir yerden yazıp yönetmek.
     - BaseClass: Sistemi ayağa kaldıracak ve kapatacak ana kontrolcü gibi düşünülebilir. Caseler koşmadan önce BaseClass tan allure yapısı tetikleniyor. Testler koştuktan sonra da log yapısı tetikleniyor. Bu yapıyı kullanarak ilerde eklemeler olunca genel sistemde değişikler yapmayıp burdan kolaylıkla yapılmasını sağlayacağını düşündüğüm için ekledim.

- **Helpers**: Sistemde birden fazla yerde kullanılacağını düşündüğüm metodları bu klasör altında topladım. Böylelikle kod tekrarını önleyip okunaklığı arttırdığını düşündüm. Küçük bir proje olduğu için şuan icerisinde Logger ve JsonConverter sınıfı var.
   - Logger: Belirli bir path içerisine gönderilen mesajların eklenmesini sağlayan methodu içeriyor.
   - JsonConverter: İçerisinde serialization-deserialization dönüşümlerini yapan methodlar bulunuyor.
   
- **Logs**: **Bu klasörün normalde burda olmaması gerekiyor.** Sadece log ve allure çıktılarını gösterebilmek için ekledim. İstenilen bir klasörün path i verilerek log ve allure kayıtlarını istediğiniz path e kaydedebilirsiniz.

- **Models**: API ye istek atarken bu isteklerin bir sınıf biçiminde olması gerekiyor. Genel olarak bu sınıfların ve bu sınıflar ile ilgili methodların yer aldığı klasördür. Bu klasörün içerisi projeye göre genişletilmelidir. Örneğin Test Suites altında Create, Update veya Search adında klasörler açıp her başlığa göre ilgili sınıfları ilgili başlık altında açmalıyız.
  - SearchResponse: Senaryoya göre  By Search yapısı kullanıldığı zaman dönen sonucların karşılığı olan sınıf yapısıdır. Search Response içerisinde dönen datalar içerisinde title a göre arama yapmamı sağlayan bir method da bulunuyor.
  - SearchResponseById: Senaryoya göre By ID or Title search yapısı kullanıldığı zaman dönen sonucların karşılığı olan sınıf yapısıdır.

- **Test Suites**: Test Caselerinin yer aldığı ve yönetildiği sınıftır. Bu klasörün içerisi projeye göre genişletilmelidir. Örneğin Test Suites altında Create, Update veya Search adında klasörler açıp caseleri anlamlı isimlendirmeler ile ayırmalıyız.
  - SearchTestCases: Senaryoya göre içinde search test case lerinin bulunduğu sınıftır.
  
- **allureConfig.Template.json**: Allure config ayarlarının bulunduğu dosyadır.
- **app.config**: Proje de zaten default bulunuyor ama ben içerisine API url, API key ve allure log path gibi sabit olan değerleri ekleyerek gerektiği zaman da buradan okudum. Bu şekilde sürekli sabit olup proje çalıştığı sırada değişmeyecek olan değerleri bir yerde toplayarak gereksiz kod tekrarını önleyip kod okunaklığını ve yönetilebilirliğini arttırdım. Sürekli sabit olan değerleri bu bölüme ekleyerek devam edebiliriz.

- **Messages.resx**: Proje genelinde case ler fail verdiği zaman gösterilecek olan hata mesajlarını tek bir yerden yazıp ilerde caseler çoğaldığı zaman geri dönüp düzeltilmesini kolaylaştıracağını düşündüm. 
![image](https://user-images.githubusercontent.com/46024317/76701471-8e03cb00-66d2-11ea-81fb-848fbd445811.png)
Burda da örnek olarak 2 tane hata mesajını görebilirsiniz. ilk sutün da projede cağrılan ismi, 2. sütunda içeriğini ve son sütunda da açıklamasını görebilirsiniz.

## Projenin Ayağa Kaldırılması
1. İlk olarak projede Allure monitoring tool kullanıldığı için bilgisayarınızda allure yüklü olmaldır. [Buradan](https://docs.qameta.io/allure/ "Buradan") indirebilirsiniz.
2. Allure çıktılarının kaydedileceği path i de kendinize göre ayarlamak için projede bulunan app.config dosyası içerisinden 'AllureLogsPath' key li değerin value sini kendi istediğiniz path ile değiştirin.

	**Not**: Bu path içinde "allure-results" isimli bir klasör olması gerekiyor.

3. Log çıktılarının kaydedileceği path i de kendinize göre ayarlamak için yine projede bulunan app.config dosyası içerisinden log4net tag i içerisindeki file value yi kendinize göre ayarlayın.
4. (**Opsiyonel**) Projeyi açmadan console üzerinden çalıştırmak için nunit-console indirmeniz gerekiyor. [Buradan](https://github.com/nunit/nunit-console/releases/tag/v3.11.1 "Buradan") indirebilirsiniz. 
Bunları yaptıktan sonra clean project ardından da rebuild yaptıktan sonra test caseleri koşturduğunuz zaman bir hata almamanız gerekiyor.

## Allure Monitoring Tool Kullanımı
Allure yapısı caseler her tamamlandıktan sonra belirtilen path e kaydediliyor. Bunları görüntülemek için komut satırını açıp "allure serve [config dosyasında ayarladığınız path]\allure-results" girerseniz browserda açılan pencere üzerinde detaylı inceleme yapabilirsiniz.
Örnek komut: 

    allure serve C:\Users\bilal\OneDrive\Masaüstü\TechTestAPI\TechTestAPI\Logs\AllureLogs\allure-results

## Nunit-Console Kullanımı
Nunit console kullanarak projeyi çalıştırmadan komut satırından testleri koşturup sonuçları görmek isterseniz komut satırını açıp "[nunitconsole.exe path] [test projesinin dll pathi("bin\debug" içerisinde bulunur)] --params=allureCleanUpType=false" seklinde komut girerek testleri koşturabilirsiniz.

Örnek Komut: 

    C:\Users\bilal\Downloads\Compressed\NUnit.Console-3.11.1\bin\net35\nunit3-console.exe C:\Users\bilal\OneDrive\Masaüstü\TechTestAPI\TechTestAPI\bin\Debug\TechTestAPI.dll --params=allureCleanUpType=false
Son bölümdeki allureCleanUpType bölümünün sebebi de her koşma başlamadan önce eski allure loglarını silmek istediğiniz zaman true yapınca eski kayıtları silerek yeniden oluşturur.
 Bu parametreler isteğe göre eklenip çoğaltılabilir...
Komut satırına girdikten sonra caseler koşmaya başlar ve tamamlandığı zaman yine komut satırı üzerinden size sonuçları gösterir. Daha detaylı bilgi almak isterseniz de komut satırını çalıştırdığınız konuma('C:\Users\UserName' olur genelde) 'TestResult.xml' isimli bir xml dosyası oluşturur. [Buradan](https://nunit.org/docs/2.6.2/nunit-console.html "Buradan") detaylı inceleyebilirsiniz.

## Örnek Ekran Çıktıları
 - **Nunit console örnek ekran çıktıları**
      - Succes Durumu:
	      ![image](https://user-images.githubusercontent.com/46024317/76702582-d8d61080-66db-11ea-80df-042862eba606.png)
	  - Fail Durumu:
	     ![image](https://user-images.githubusercontent.com/46024317/76702636-46823c80-66dc-11ea-87b2-cc212652d33c.png)

- **Log Yapısının örnek çıktıları**

     ![image](https://user-images.githubusercontent.com/46024317/76702679-b264a500-66dc-11ea-8cbd-d4063cec86de.png)
- **Allure Yapısının Örnek çıktıları**
     - Genel Sonuçları Overview bölümünden takip edebilirsiniz:![image](https://user-images.githubusercontent.com/46024317/76702731-40d92680-66dd-11ea-8bf9-18066ba6b501.png)
   
     - TestCaseleri Suites Bazında görebilirsiniz:![image](https://user-images.githubusercontent.com/46024317/76702743-58b0aa80-66dd-11ea-8233-c3166e0f9a26.png)  
  
     - Testin Detaylarını Overview den görebilirsiniz:![image](https://user-images.githubusercontent.com/46024317/76702837-25bae680-66de-11ea-98e1-a137a3141b45.png)
  
     - Daha önceki sonuçlarını Retries bölümünden görebilirsiniz:![image](https://user-images.githubusercontent.com/46024317/76702886-77fc0780-66de-11ea-8819-0ac32e7d18cb.png)
  
  Yukarıdaki ekranlar dışında allure tool unun daha birçok özelliği  bulunmaktadır. Detaylı bilgi almak [buraya](https://docs.qameta.io/allure/#_report_structure "burdan") bakabilirsiniz.
