--1) Показать самую дорогую книгу в жанре Psychology
select top 1 * from titles
where type = 'psychology'
order by price desc

--2) Показать жанр, в котором написано больше всего книг
select top 1 type
from titles
group by type
order by  count(*) desc

--3) Показать дату публикации самой дешёвой книги в жанре business
select top 1 pubdate from titles
where price is not null
order by price

--4) Показать штат, в котором проживает больше всего авторов
select top 1 state from authors
group by state
order by count(*) desc

--5) Показать жанр, в котором опубликована самая дорогая книга
select top 1 type from titles
order by price desc

--6) Увеличить цену книг в жанре business на 2 процента
select price from titles
update titles
set price = price + (price / 100 * 2)
where type = 'business'


--7) Показать количество книг с названием меньше 10 букв 
select * from titles
select count(*) from titles
where len(title) < 10