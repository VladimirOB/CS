5. �������� ������� �� ����� CA, ������� �� �������� �� ���������
select * from authors
where state = 'CA' and contract = 0;


6. �������� ��� ����� � ����� 'Business', ������� ����� ������ $20
select * from titles 
where type = 'business' and price < 20

7. �������� ���� ������� �� ����� CA, 
������������� �� ��������� � �� au_fname � ������� ��������
select * from authors
where state = 'CA' 
order by contract, au_fname desc

8. �������� ��� ����� � ��������� [$5, $15], � ����� Psychology
select * from titles
where type = 'psychology'
and price between 5 and 15

9. �������� ������� ����� ����� ������� � ����� ������� ������ � ����� 'Business'
select max(price) 'max', min(price) as 'min' from titles
where type = 'business'

select max(price) -  min(price) '�������' from titles
where type = 'business'