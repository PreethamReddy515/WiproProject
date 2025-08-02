
-- HMS: Hospital Management System

-- Table creation scripts

CREATE DATABASE HospitalDB;
GO
USE HospitalDB;
GO

CREATE TABLE DOCTOR_MASTER (
    doctor_id VARCHAR(15) PRIMARY KEY,
    doctor_name VARCHAR(15) NOT NULL,
    dept VARCHAR(15) NOT NULL
);

CREATE TABLE ROOM_MASTER (
    room_no VARCHAR(15) PRIMARY KEY,
    room_type VARCHAR(15) NOT NULL,
    status VARCHAR(15) NOT NULL
);

CREATE TABLE PATIENT_MASTER (
    pid VARCHAR(15) PRIMARY KEY,
    name VARCHAR(15) NOT NULL,
    age INT NOT NULL,
    weight INT NOT NULL,
    gender VARCHAR(10) NOT NULL,
    address VARCHAR(50) NOT NULL,
    phoneno VARCHAR(10) NOT NULL,
    disease VARCHAR(50) NOT NULL,
    doctorid VARCHAR(15) NOT NULL,
    FOREIGN KEY (doctorid) REFERENCES DOCTOR_MASTER(doctor_id)
);

CREATE TABLE ROOM_ALLOCATION (
    room_no VARCHAR(15),
    pid VARCHAR(15),
    admission_date DATE NOT NULL,
    release_date DATE,
    FOREIGN KEY (room_no) REFERENCES ROOM_MASTER(room_no),
    FOREIGN KEY (pid) REFERENCES PATIENT_MASTER(pid)
);

-- Sample data insertion

INSERT INTO DOCTOR_MASTER VALUES 
('D0001','Ram','ENT'),
('D0002','Rajan','ENT'),
('D0003','Smita','Eye'),
('D0004','Bhavan','Surgery'),
('D0005','Sheela','Surgery'),
('D0006','Nethra','Surgery');

INSERT INTO ROOM_MASTER VALUES 
('R0001','AC','occupied'),
('R0002','Suite','vacant'),
('R0003','NonAC','vacant'),
('R0004','NonAC','occupied'),
('R0005','AC','vacant'),
('R0006','AC','occupied');

INSERT INTO PATIENT_MASTER VALUES 
('P0001','Gita',35,65,'F','Chennai','9867145678','Eye Infection','D0003'),
('P0002','Ashish',40,70,'M','Delhi','9845675678','Asthma','D0003'),
('P0003','Radha',25,60,'F','Chennai','9867166678','Pain in heart','D0005'),
('P0004','Chandra',28,55,'F','Bangalore','9978675567','Asthma','D0001'),
('P0005','Goyal',42,65,'M','Delhi','8967533223','Pain in Stomach','D0004');

INSERT INTO ROOM_ALLOCATION VALUES
('R0001','P0001','2016-10-15','2016-10-26'),
('R0002','P0002','2016-11-15','2016-11-26'),
('R0002','P0003','2016-12-01','2016-12-30'),
('R0004','P0001','2017-01-01','2017-01-30');

-- HMS Queries

-- 1. Patients admitted in January
SELECT * FROM ROOM_ALLOCATION WHERE MONTH(admission_date) = 1;

-- 2. Female patients not suffering from Asthma
SELECT * FROM PATIENT_MASTER WHERE gender = 'F' AND disease NOT LIKE '%Asthma%';

-- 3. Count male and female patients
SELECT gender, COUNT(*) AS count FROM PATIENT_MASTER GROUP BY gender;

-- 4. Patient + doctor + room info
SELECT 
    p.pid AS patient_id,
    p.name AS patient_name,
    d.doctor_id,
    d.doctor_name,
    ra.room_no,
    rm.room_type,
    ra.admission_date
FROM ROOM_ALLOCATION ra
JOIN PATIENT_MASTER p ON ra.pid = p.pid
JOIN DOCTOR_MASTER d ON p.doctorid = d.doctor_id
JOIN ROOM_MASTER rm ON ra.room_no = rm.room_no;

-- 5. Rooms never allocated
SELECT room_no FROM ROOM_MASTER
WHERE room_no NOT IN (SELECT DISTINCT room_no FROM ROOM_ALLOCATION);

-- 6. Rooms allocated more than once
SELECT room_no, COUNT(*) AS times_allocated
FROM ROOM_ALLOCATION
GROUP BY room_no
HAVING COUNT(*) > 1;
