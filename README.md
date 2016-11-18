# Creative Hub
Курсовой проект на курсах по разработке ПО компании iTransition
	
Проект включает:
* 1. Серверная часть - ASP.NET WebApi, MS SQLEXPRESS,  Entity Framework 6.
* 2. Клиентская часть - AngularJS.
* 3. CSS-framework - Bootstrap 3.
* 4. Token Based Authentication с использованием Owin и Identity, с разделением ролей пользователей.
* 5. Каждый пользователь имеет личную страницу, имя автар, список его контента и достжения (achievements)
* 6. Пользователи создают креатив, который включает название, категорияб описание, набор тегов (с автодополнением) и набор глав.
* 7. Каждая глава имеет свой порядковый номер, который можно изменять drag&drop-ом, поддреживается редактор текста в формате Markdown.
* 8. При входе на страницу пользователя отображается спиок произведения, поддерживается поиск по произведениям, сортировка по дате,
рейтингу, популярности произведения.
* 9. На главной странице сайта отображаются последние произведения, произведения с наибольшим рейингом, самые популярные произведения,
список самых популярных тегов.
* 10. Поддрерживается функция полнотекстового поиска (с фильтром параметров) - Lucene.Net.
* 11. Аутентифицированные пользователи могут выставлять рейтинг произведения, оставлять комментарии, оценивать камментарии с помощи "лайков".
* 12. Произведения поддерживает режим чтения(с некоторыми параметрами) для всех пользовтелей с функцией сохранения последней прочитанной главы. 
* 13. Реализована поддержка 2 тем для всего сайта.
* 14. Реализована поддержка 2 языков - русского и английского.
* 15. Выбранный язык и текущая тема сохраняется в LocalStorage, сохраненная позиция чтения произведения в БД.

## Особенности реализации серверной части
1. Паттерн UnitOfWork
2. DI контейнер Ninject
3. Cледование SOLID принципам

[Creative Hub Web Deploy](http://sweexxik-001-site1.anytempurl.com)