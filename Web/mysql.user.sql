CREATE USER 'nano-template-web-user'@'%' IDENTIFIED BY '<<secret>>';
GRANT SELECT, INSERT, UPDATE, DELETE ON webDb.* TO 'nano-template-web-user'@'%';
FLUSH PRIVILEGES;