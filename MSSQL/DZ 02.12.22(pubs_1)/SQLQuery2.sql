--1) �������� ����� ������� ����� � ����� Psychology
select top 1 * from titles
where type = 'psychology'
order by price desc

--2) �������� ����, � ������� �������� ������ ����� ����
select top 1 type
from titles
group by type
order by  count(*) desc

--3) �������� ���� ���������� ����� ������� ����� � ����� business
select top 1 pubdate from titles
where price is not null
order by price

--4) �������� ����, � ������� ��������� ������ ����� �������
select top 1 state from authors
group by state
order by count(*) desc

--5) �������� ����, � ������� ������������ ����� ������� �����
select top 1 type from titles
order by price desc

--6) ��������� ���� ���� � ����� business �� 2 ��������
select price from titles
update titles
set price = price + (price / 100 * 2)
where type = 'business'


--7) �������� ���������� ���� � ��������� ������ 10 ���� 
select * from titles
select count(*) from titles
where len(title) < 10