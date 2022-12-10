5. Показать авторов из штата CA, которые не работают по контракту
select * from authors
where state = 'CA' and contract = 0;


6. Показать все книги в жанре 'Business', которые стоят меньше $20
select * from titles 
where type = 'business' and price < 20

7. Показать всех авторов из штата CA, 
отсортировать по контракту и по au_fname в порядке убывания
select * from authors
where state = 'CA' 
order by contract, au_fname desc

8. Показать все книги в диапазоне [$5, $15], в жанре Psychology
select * from titles
where type = 'psychology'
and price between 5 and 15

9. Показать разницу между самой дорогой и самой дешёвой книгой в жанре 'Business'
select max(price) 'max', min(price) as 'min' from titles
where type = 'business'

select max(price) -  min(price) 'Разница' from titles
where type = 'business'