# MVC05Homework01
[Toc]

## Charter I
疑問
- 搜尋功能可不可以作所有欄位？ 
    可以，包兩層Model?
- Where多條件的情況要怎麼做？
    可以把IQUEYABLE包成Extension然後針對每個Query作條件判斷？
    query = query.客戶名稱Filter();
    query = query.EmailFilter();
    
- 排序功能自己重做一次

- 篩選功能是什麼意思？

//20190524
- Edit改用法
    - 如果有欄位不能重覆(Only Create) 、 但是Edit可以 要怎麼做？
    - 呈上，為什麼用Model驗證，結果跑出的結果是ViewModel的驗證呢？
    - 然後如果TryUpdateModel和TryValidateModel驗證的是ViewModel的話 Commit()不會更新DB..? WHY?

- 整理ViewModel <--> Model 放到Action Filter裡面？
- 匯出功能 怎麼樣讓搜尋結果也可以丟過去？把資料List包進beginForm嗎？



### Detail
 1. 資料庫下載
 2. `12min`請使用 "客戶資料" 這個資料庫做開發 (如附件檔案)
找了一下怎麼匯入mdf，然後一開始以為中文檔名不行，後來才想到要加N'fileName'
```sql=
EXEC  sp_attach_db  @dbname=N'客戶資料',     
@filename1  =  N'絕對路徑\客戶資料.mdf',     
@filename2  =  N'絕對路徑\客戶資料_log.ldf'
```
 3. 請實作出「客戶資料管理」、「客戶聯絡人管理」與「客戶銀行帳戶管理」等簡易 CRUD 功能 (盡量做各種功能出來)
- [x]14min「客戶資料管理」(基本CRUD)
- [x]8min「客戶聯絡人管理(基本CRUD,對應客戶資料)
- [x]7min「客戶銀行帳戶管理」
 4. 在列表頁要實作「搜尋」功能
- [x]`13min`「客戶資料管理」(單一欄位搜尋)
- [x]「客戶聯絡人管理」(單一欄位搜尋)
- [x]`3min`「客戶銀行帳戶管理」(單一欄位搜尋)
 5. `31min`實作一個清單頁面，顯示欄位有「客戶名稱、聯絡人數量、銀行帳戶數量」共三個欄位，此功能要在資料庫中建立一個檢視表，並且匯入 EDMX。
- [x]`3min`在資料庫中建立一個檢視表，並且匯入 EDMX。
- [x]`3min`欄位有「客戶名稱、聯絡人數量、銀行帳戶數量」
- [x]`12min`清單頁面(新增檢視List,沒有自動產生對應欄位？是因為是SQL View的關係嗎？)
- [x]`13min`這邊多加個客戶資料.ID 這樣可以連去客戶資料CRUD XD
 6. `1min`主選單要有連結可以直接連到三個主要功能的列表頁。
 7. 對於 Create 與 Edit 表單要套用欄位驗證 (必填、Email格式、欄位長度限制等驗證)
 8. `19min`刪除資料功能不能真的刪除資料庫中的資料，必須修改資料庫，加上一個「是否已刪除」欄位，資料庫中的該欄位為 bit 類型，0 代表未刪除，1 代表已刪除，且列表頁不得顯示已刪除的資料。
 9. `12min`實作「客戶聯絡人」時，同一個客戶下的聯絡人，其 Email 不能重複。
 10.`10min`實作一個「自訂輸入驗證屬性」可驗證「手機」的電話格式必須為 "\d{4}-\d{6}" 的格式 ( e.g. 0911-111111 )
 11. 使用 Repository Pattern 管理所有新刪查改(CRUD)等功能
 12. 修改所有的「刪除」邏輯，所有資料都不能真正被刪除，資料庫中也要新增「是否已刪除」欄位 (欄位要設定 bit 型態)
 13. `40min`修改「客戶資料」表格，新增「客戶分類」欄位，可設定特定選項的分類(針對CRUD改用ViewModel)
 14. `13min`在「客戶資料列表」頁面新增篩選功能，可以用「客戶分類」欄位進行資料篩選 (下拉選單) :::	warning 先用了很簡單ENUM 和寫死的if-else判斷 以前用sql可以判斷有沒有直 +SQL 但如果是強型別的話要怎麼處理呢？ 可以用Func嗎？ :::
 15. 資料篩選的邏輯要寫在 Repository 的類別裡面
 16. `25min`在「客戶聯絡人列表」頁面新增篩選功能，可以用「職稱」欄位進行資料篩選
 17. 修改「客戶資料列表」與「客戶聯絡人列表」頁面，設定讓每個欄位都能進行排序 (可升冪、可降冪排序)
 - `27min`「客戶資料列表」頁面
 - `5min` 找了一下網路上的範例改來用，原理大致理解，再來是要在+Search功能回來，rep最後一步才做Order，要再找時間自己重寫一次。
 - `50min` 把Sort功能改成Extension本來想改成擴充泛型，結果原來不能這樣用。這樣好像沒必要Sort拉出來？
 - 「客戶聯絡人列表」頁面
 18. 如果可以的話，透過 JavaScript 實作一些 AJAX 操作，後端 MVC 使用 JsonResult 進行回應
 19. 使用 ClosedXML 這個 NuGet 套件實作資料匯出功能，每個清單頁上都要有可以匯出 Excel 檔案的功能，要用到 FileResult 下載檔案
- [x]`90min`客戶資料
 - `7min`客戶聯絡人
 - `7min`客戶銀行醫訊

### Reference
- [ClosedXML教學](https://www.aspsnippets.com/Articles/ClosedXML-MVC-Example-Export-to-Excel-using-ClosedXML-in-ASPNet-MVC.aspx)