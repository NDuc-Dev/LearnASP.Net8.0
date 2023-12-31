# LearnASP.Net8.0


**Mô hình của dotnet MVC:**
* M (Model) : là lớp đảm nhiệm việc quản lí dữ liệu và nghiệp vụ của ứng dụng.
    + Chức năng:
        - Quản lí dữ liệu: lưu trữ và truy cập dữ liệu ứng dụng.
        - Nghiệp vụ: Thực hiện các xử lí logic và nghiệp vụ.

* V (View) : Là thành phẩn đảm nhiệm việc hiển thị thông tin cho người dùng và nhận các sự kiện tương tác.
    + Chức năng: 
        - Hiển thị dữ liệu: định dạng và hiển thị dữ liệu từ model cho người dùng.
        - Tương tác với người dùng: Nhận sự kiện từ người dùng như nhấp vào nủ hoặc điều khiển mẫu.
* C (Controller) : là thành phần điều phối tương tác giữa Model và View
    + Chức năng: 
        - Xử lí yêu cầu : Nhận yêu cầu từ người dùng thông qua View và xác định các hành động cần thực hiện.
        - Tương tác với Model : Gọi các phương thức từ Model để thực hiện việc truy vấn dữ liệu.
        - Cập nhật View : Chọn View phù hợp để hiển thị kết quả cho người dùng.

LUỒNG TỔNG QUÁT:  1. Người dùng tương tác với View.
                  2. View gửi yêu cầu đến Controller.
                  3. Controller xử lí yêu cầu, tương tác với Model (nếu cần), chọn View phù hợp.
                  4. View hiển thị thông tin cho người dùng hoặc thu thập thông tin từ người dùng.
                  5. Lặp lại quy trình khi có tương tác mới.


**Các bước thực hiện với dự án MVC trên VSCode**

* Tạo dự án: Tạo thư mục lưu trữ dự án => mở cmd => cli: **dotnet new mvc -o ProjectName**

* Tin tưởng chứng chỉ phát triển HTTPS bằng cli : **dotnet dev-certs HTTPS --trust**

* Chọn Command Palette .Net: Chọn View => Command Palette => nhập .Net => chọn **.Net: Generate Asset for build and debug.**

* Add các package của EntityFramework và các tool cần thiết để thực hiện tương tác với database sử dụng (Vd: SQLServer)
    + dotnet tool install --global dotnet-aspnet-codegenerator
    + dotnet tool install --global dotnet-ef
    + dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    + dotnet add package Microsoft.EntityFrameworkCore
    + dotnet add package Microsoft.EntityFrameworkCore.Design
    + dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    + dotnet add package Microsoft.EntityFrameworkCore.Tools
    + dotnet restore

* Thêm Dbcontext: ở thư mục dự án thêm thư mục Data => thêm file **FilenameDbcontext.cs** và cấu hình file như file ApplicationDbContext như mẫu của dự án này và đăng kí service vào file Program.cs

* Thêm Connection string : 
    + mở file appsetting.json và thêm cấu hình kết nối với db như sau:
        - "ConnectionStrings":{
    "DefaultConnection": "Server=ServerInfo; Database=DbName ;Trusted_Connection=True;TrustServerCertificate=True"
  }

    trong đó: 
        + **ServerInfo** là server cần kết nối đến, **DbName** là tên của database 

* Thêm các role (Nếu có):
    + sử dụng lệnh **dotnet-aspnet-codegenerator area RoleName** để tạo role mong muốn.


* Thêm Model : tạo thư mục trong Folder Models của Project => thêm name space, Tên Model và các thuộc tính của Model.
            
* Cập nhật database:
        + sử dụng **dotnet ef migrations add Name --verbose --project ProjectName --startup-project ProjectStartupName** để thêm một migrations trong đó **Name** là tên của migrations có thể thay đổi để lưu lại quá trình cập nhật, **ProjectName** là thư mục dự án chứa file dbcontext và **ProjectStartupName** là thư mục dự án chính.
        + sử dụng **dotnet ef database update --verbose --project ProjectName --startup-project ProjectStartupName** để cập nhật dữ liệu trong database

    **Lưu ý: Khi có bất cứ cập nhật nào trong dữ liệu liên quan đến data, cần thực hiện lệnh này để đồng bộ hoặc tạo mới dữ liệu trong database**

* Tạo thêm Controller và View theo Model vừa được tạo:
        + sử dụng Cli :
        **dotnet aspnet-codegenerator controller -name NameController -m Name -dc YourDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --force**

        + -name NameController: Tên của controller sẽ được tạo
        + -m Name: Tên của model được liên kết với controller
        + -dc YourDbContext: Tên của DbContext mà bạn muốn sử dụng.
        + --relativeFolderPath Controllers: Đường dẫn tương đối đến thư mục chứa controller.
        + --useDefaultLayout: Sử dụng layout mặc định cho controller.
        + --referenceScriptLibraries: Thêm thư viện script cần thiết.
        + --force: Ghi đè lên controller nếu nó đã tồn tại.
