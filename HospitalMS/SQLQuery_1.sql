create database hospitaldb


use hospitaldb


create table patient (
    patientid int primary key identity(1,1),
    firstname varchar(100) not null,
    lastname varchar(100) not null,
    dateofbirth date not null,
    gender varchar(10) not null,
    contactnumber varchar(15),
    address varchar(255)
)

create table doctor (
    doctorid int primary key identity(1,1),
    firstname varchar(100) not null,
    lastname varchar(100) not null,
    specialization varchar(100) not null,
    contactnumber varchar(15)
)

create table appointment (
    appointmentid int primary key identity(1,1),
    patientid int not null,
    doctorid int not null,
    appointmentdate datetime not null,
    description text,
    foreign key (patientid) references patient(patientid),
    foreign key (doctorid) references doctor(doctorid)
)

insert into patient (firstname, lastname, dateofbirth, gender, contactnumber, address) 
values 
('Ram', 'Das', '1985-06-15', 'Male', '9234567890', '123 Khazana St'),
('Diksha', 'Sharma', '1990-08-22', 'Female', '0987654321', '456 Lahori Gate'),
('Mukesh', 'Patil', '1978-02-12', 'Male', '9345678901', '789 Hathi Chowk')


insert into doctor (firstname, lastname, specialization, contactnumber) 
values 
('Rakesh', 'Sharma', 'Cardiologist', '8456789012'),
('Chirag', 'Bhatt', 'Neurologist', '7567890123'),
('Kartik', 'Pathak', 'Dermatologist', '6678901234')


insert into appointment (patientid, doctorid, appointmentdate, description) 
values 
(1, 1, '2024-10-05 10:00:00', 'Routine check-up for heart health'),
(2, 2, '2024-10-06 14:00:00', 'Migraine follow-up'),
(3, 3, '2024-10-07 16:30:00', 'Skin rash consultation')


select * from patient
select * from doctor
select * from appointment
