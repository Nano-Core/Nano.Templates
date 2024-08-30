CREATE USER 'nano-template-service-user'@'%' IDENTIFIED BY '<<secret>>';
GRANT SELECT, INSERT, UPDATE, DELETE ON serviceDb.* TO 'nano-template-service-user'@'%';
FLUSH PRIVILEGES;