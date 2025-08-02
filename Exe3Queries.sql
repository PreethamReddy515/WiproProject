--1.Query #1: Display the patients who were admitted in the month of January
SELECT * 
FROM ROOM_ALLOCATION
WHERE MONTH(admission_date) = 1;


--2.Display the female patient who is not suffering from asthma
SELECT * 
FROM PATIENT_MASTER
WHERE gender = 'F' AND disease NOT LIKE '%Asthma%';

--3: Count the number of male and female patients
SELECT gender, COUNT(*) AS total
FROM PATIENT_MASTER
GROUP BY gender;

--4.4: Display patient_id, patient_name, doctor_id, doctor_name, room_no, room_type and admission_date
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


--5 Display the room_no which was never allocated to any patient
SELECT room_no 
FROM ROOM_MASTER
WHERE room_no NOT IN (
    SELECT DISTINCT room_no FROM ROOM_ALLOCATION
);

--6.Display the room_no, room_type which are allocated more than once
SELECT rm.room_no, rm.room_type, COUNT(*) AS times_allocated
FROM ROOM_ALLOCATION ra
JOIN ROOM_MASTER rm ON ra.room_no = rm.room_no
GROUP BY rm.room_no, rm.room_type
HAVING COUNT(*) > 1;


