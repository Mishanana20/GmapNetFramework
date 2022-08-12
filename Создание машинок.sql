USE TestTask_Gmap;
DELETE FROM markers;
INSERT INTO markers (name, longitude, latitude) 
VALUES 
('трактор', 83.742, 55.36),
('автокран', 83.744, 55.353),
('бульдозер', 83.741, 55.34);
SELECT * FROM markers
go