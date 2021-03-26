	
	CREATE TABLE kfztyp (
	  idkfztyp int(11) NOT NULL AUTO_INCREMENT,
	  Typ text DEFAULT NULL,
	  PRIMARY KEY (idkfztyp)
	) ENGINE=InnoDB DEFAULT CHARSET=latin1;

	CREATE TABLE kfz (
	  idkfz int(11) NOT NULL AUTO_INCREMENT,
	  kfztyp_idkfztyp int(11) NOT NULL,
	  FahrgestellNr text DEFAULT NULL,
	  Kennzeichen text DEFAULT NULL,
	  Leistung int(11) DEFAULT NULL,
	  PRIMARY KEY (idkfz)
	) ENGINE=InnoDB DEFAULT CHARSET=latin1;

	ALTER TABLE kfz
	  ADD CONSTRAINT fk_kfz_kfztyp 
	  FOREIGN KEY (kfztyp_idkfztyp) 
	  REFERENCES kfztyp (idkfztyp) ON DELETE NO ACTION ON UPDATE NO ACTION;


	INSERT INTO kfztyp (idkfztyp, Typ) 
	VALUES
	(1, 'Limousine'),
	(2, 'Cabrio'),
	(3, 'Kraftrad'),
	(4, 'LKW');

	INSERT INTO kfz (idkfz, kfztyp_idkfztyp, FahrgestellNr, Kennzeichen, Leistung) 
	VALUES
	(1, 2, '198231729831', 'S-KU-253', 120),
	(2, 1, '34535435', 'KA-FA-765', 75),
	(3, 4, '2345235435', 'WN-ZU-876', 100);



