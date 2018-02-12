To install DB
1. run in Package Manager Console
   PM> update-database -project Sattelite.EntityFramework
   (then look in SQL Server if SatteliteDB database created)

2. Run application Sattelite.Web in IIS Express. If it is running, click 'Login' on the right top corner of the site
2.1 to login as administrator enter
  Login: Admin
  Password: Admin
2.2 to login with social network, click twitter, etc/ then - confirm

3. There are 2 boards 
3.1 on Admin Board can be
 - created\removed categories
 - created\removed projects
 - created\removed users, and added\removed Category subscriptions per user
 
3.2 On main board User may see all news\projects he\she is subscribed (via admin board).
But he\she can subscibe himself by clickin on site botton links (with categories names)