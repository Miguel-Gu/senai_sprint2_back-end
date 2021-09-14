USE CATALOGO_T;
GO

CREATE TABLE USUARIOS(
	idUsuario INT PRIMARY KEY IDENTITY,
	email VARCHAR(100) NOT NULL UNIQUE,
	senha VARCHAR(100) NOT NULL,
	permissao VARCHAR(30) NOT NULL
);
GO

INSERT INTO USUARIOS (email, senha, permissao)
VALUES ('lucas@email.com', '123', 'comum'),
	   ('adm@email.com', '123', 'administrador');
GO

SELECT * FROM USUARIOS

	SELECT * FROM USUARIOS WHERE email = 'adm@email.com' AND senha = '123'