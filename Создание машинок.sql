USE TestTask_Gmap;
DELETE FROM markers;
INSERT INTO markers (name, longitude, latitude) 
VALUES 
('�������', 83.742, 55.36),
('��������', 83.744, 55.353),
('���������', 83.741, 55.34);
SELECT * FROM markers
go