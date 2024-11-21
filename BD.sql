-- Projet final de BD
-- Justin Bélanger & Florence Blackburn

-- RESET
DROP TABLE IF EXISTS participation;
DROP TABLE IF EXISTS adherent;
DROP TABLE IF EXISTS seance;
DROP TABLE IF EXISTS activite;
DROP TABLE IF EXISTS categorie;


-- CREATE TABLE -> Florence
CREATE TABLE categorie(
    nom VARCHAR(155),
    PRIMARY KEY pk_categorie (nom)
);

CREATE TABLE activite(
    nom VARCHAR(155),
    type VARCHAR(155),
    coutOrganisation DOUBLE,
    prixVente DOUBLE,
    PRIMARY KEY pk_activite (nom, type),
    FOREIGN KEY fk_activite_categorie (type) REFERENCES categorie (nom)
);

CREATE TABLE seance(
    idSeance INT AUTO_INCREMENT,
    activiteNom VARCHAR(155),
    activiteType VARCHAR(155),
    PRIMARY KEY pk_seance (idSeance),
    FOREIGN KEY fk_seance_activite (activiteNom, activiteType) REFERENCES activite(nom, type)
);

CREATE TABLE adherent(
    noIdentification VARCHAR(11),
    nom VARCHAR(155),
    prenom VARCHAR(155),
    adresse VARCHAR(255),
    dateNaissance DATETIME,
    age INT,
    email VARCHAR(255),
    pseudo VARCHAR(155),
    mdp VARCHAR(255),
    role VARCHAR(155),
    PRIMARY KEY pk_adherent (noIdentification)
);

CREATE TABLE participation(
    idParticipation INT AUTO_INCREMENT,
    idAdherent VARCHAR(11),
    idSeance INT,
    note DOUBLE,
    PRIMARY KEY pk_participation (idParticipation),
    FOREIGN KEY fk_participation_adherent (idAdherent) REFERENCES adherent(noIdentification),
    FOREIGN KEY fk_participation_seance (idSeance) REFERENCES seance(idSeance)
);


-- Déclencheurs


-- Insertion

-- Vues

-- Procédures

-- Fonctions




