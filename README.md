# UploadFile
1. Giới thiệu đề tài
Trong thời đại công nghệ ngày nay, việc lưu trữ và chia sẻ dữ liệu qua internet đã trở thành một phần quan trọng của cuộc sống và công việc hàng ngày. Đối với những nhà phát triển ứng dụng, việc tích hợp các dịch vụ lưu trữ đám mây như Google Drive không chỉ mang lại lợi ích về việc quản lý dữ liệu mà còn tối ưu hóa trải nghiệm người dùng.
Đề tài “Lập trình ứng dụng Upload file lên Google Drive” chúng em sẽ khám phá cách lập trình một ứng dụng sử dụng ngôn ngữ lập trình C# và giao diện WinForms để thực hiện việc tải lên (upload) các file lên Google Drive thông qua API của Google Drive, sử dụng thư viện Google.Apis.Drive.v3. Ngoài ra, chúng em còn sử dụng Multi-thread để việc uploads nhanh hơn.
Qua đề tài này, chúng em hy vọng mang lại cái nhìn tổng quan và kiến thức cần thiết để phát triển ứng dụng tích hợp Google Drive một cách hiệu quả và linh hoạt bằng ngôn ngữ lập trình C# và giao diện WinForms.
2. Phương pháp nghiên cứu
Nghiên cứu lý thuyết về Google Drive API, Google.Apis.Drive.v3, Multi-thread thông qua tài liệu của các diễn đàn và các hướng dẫn bằng tài liệu cả trong nước và nước ngoài.
Áp dựng các kiến thức đã được học và thực hành ở môn Lập trình mạng như lập trình WinForms, mô hình TCP/UDP.
Phân tích yêu cầu chức năng, xác định các chức năng chính mà ứng dụng cần thực hiện, chẳng hạn như tải lên một hoặc nhiều file và cả thư mục cũng như cho phép người dùng thực hiện kéo thả.
Tổng hợp các kiến thức và trải nghiệm thành báo cáo và xây dựng ứng dụng Upload file lên Google Drive hoàn chỉnh.
3. Kết quả đạt được khi thực hiện đề tài
Tích Hợp Thành Công với Google Drive: Ứng dụng của bạn đã có thể tích hợp một cách thành công với Google Drive thông qua API sử dụng thư viện Google.Apis.Drive.v3. Người dùng có khả năng tải lên các tệp tin từ máy tính của họ lên Google Drive một cách thuận lợi.
Giao Diện Người Dùng Thân Thiện: Đã tạo ra một giao diện người dùng sử dụng WinForms, cung cấp một trải nghiệm thân thiện và dễ sử dụng. Giao diện này giúp người dùng tương tác một cách tự nhiên với ứng dụng và thực hiện các thao tác tải lên một cách dễ dàng.
Sử dụng Multi-thread để tối ưu tốc độ uploads.
Nâng Cao Kỹ Năng Lập Trình và Phát Triển Phần Mềm: Đề tài này đã mang lại cơ hội để nâng cao kỹ năng lập trình C# và phát triển ứng dụng có tính ứng dụng cao, từ đó củng cố kiến thức về lập trình mạng và tích hợp API vào ứng dụng.
4. Quy trình xác thực người dùng
Trong ứng dụng của chúng em, quy trình xác thực người dùng được thực hiện thông qua hai tệp tin quan trọng là credentials.json và token.json. 
