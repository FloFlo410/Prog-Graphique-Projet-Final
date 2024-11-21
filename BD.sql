-- Projet final de BD
-- Justin Bélanger & Florence Blackburn

-- RESET ------------------------------------------------------------------------------
DROP TABLE IF EXISTS participation;
DROP TABLE IF EXISTS adherent;
DROP TABLE IF EXISTS seance;
DROP TABLE IF EXISTS activite;
DROP TABLE IF EXISTS categorie;


-- CREATE TABLE -> Flo ------------------------------------------------------------------------------
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


-- Déclencheurs ------------------------------------------------------------------------------

-- Permet de construire le numéro d’identification de l’adhérent lors de son insertion -> Flo
DROP TRIGGER IF EXISTS generer_noIdentication_adherent;
DELIMITER //
CREATE TRIGGER generer_noIdentication_adherent BEFORE INSERT ON adherent FOR EACH ROW
    BEGIN
        SET NEW.noIdentification = CONCAT(
            SUBSTR(NEW.prenom, 1,1),
            SUBSTR(NEW.nom, 1,1),
            '-',
            YEAR(dateNaissance),
            '-',
            ROUND((RAND() * (9))),
            ROUND((RAND() * (9))),
            ROUND((RAND() * (9)))
            );
    end //
delimiter ;

-- Permet de calculer l'age lors de l'insertion d'un adherent -> Flo
DROP TRIGGER IF EXISTS calculer_age_adherent;
DELIMITER //
CREATE TRIGGER calculer_age_adherent BEFORE INSERT ON adherent FOR EACH ROW
    BEGIN
        SET NEW.age = YEAR(DATEDIFF(CURDATE(), dateNaissance));
    end //
delimiter ;

-- Permet de gérer le nombre de places disponibles dans chaque séance
DROP TRIGGER IF EXISTS gerer_nbPlaceDispo_seance;
DELIMITER //
CREATE TRIGGER gerer_nbPlaceDispo_seance AFTER INSERT ON participation FOR EACH ROW
    BEGIN

    end //
delimiter ;

/*
 Permet d’insérer les participants dans une séance si le nombre de places maximum n’est pas atteint.
 Sinon, il affiche un message d’erreur avisant qu’il ne reste plus de places disponibles pour la
 séance choisie.
 */



-- Insertion -> Justin ------------------------------------------------------------------------------
INSERT INTO categorie (nom) VALUES
('Sports'),
('Musique'),
('Art'),
('Cuisine'),
('Loisirs créatifs'),
('Musculation'),
('Technologie'),
('Voyages'),
('Santé'),
('Éducation');

INSERT INTO activite (nom, type, coutOrganisation, prixVente) VALUES
('Football', 'Sports', 1000.00, 50.00),
('Yoga', 'Musculation', 500.00, 30.00),
('Concert', 'Musique', 2000.00, 100.00),
('Peinture', 'Art', 300.00, 20.00),
('Pâtisserie', 'Cuisine', 400.00, 40.00),
('Danse', 'Loisirs créatifs', 600.00, 35.00),
('Programmation', 'Technologie', 800.00, 75.00),
('Randonnée', 'Voyages', 300.00, 20.00),
('Méditation', 'Santé', 150.00, 25.00),
('Cours de langue', 'Éducation', 700.00, 45.00);

INSERT INTO seance (activiteNom, activiteType) VALUES
('Football', 'Sports'),
('Yoga', 'Musculation'),
('Concert', 'Musique'),
('Peinture', 'Art'),
('Pâtisserie', 'Cuisine'),
('Danse', 'Loisirs créatifs'),
('Programmation', 'Technologie'),
('Randonnée', 'Voyages'),
('Méditation', 'Santé'),
('Cours de langue', 'Éducation');




INSERT INTO adherent (noIdentification, nom, prenom, adresse, dateNaissance, age, email, pseudo, mdp, role) VALUES
('A123456789', 'Dupont', 'Pierre', '123 rue des Fleurs', '1990-05-12', 34, 'pierre.dupont@email.com', 'pierreD', 'mdp123', 'administrateur'),
('B987654321', 'Martin', 'Claire', '456 avenue des Champs', '1985-08-20', 39, 'claire.martin@email.com', 'claireM', 'mdp456', 'utilisateur'),
('C246810121', 'Durand', 'Julien', '789 boulevard Saint-Michel', '2000-01-15', 24, 'julien.durand@email.com', 'julienD', 'mdp789', 'utilisateur'),
('D135791113', 'Lemoine', 'Sophie', '234 rue de la Paix', '1993-11-10', 31, 'sophie.lemoine@email.com', 'sophieL', 'mdp321', 'administrateur'),
('E246802468', 'Girard', 'Thomas', '567 rue du Parc', '1992-04-25', 32, 'thomas.girard@email.com', 'thomasG', 'mdp654', 'utilisateur'),
('F112233445', 'Petit', 'Alice', '890 avenue de la Liberté', '1988-02-12', 36, 'alice.petit@email.com', 'aliceP', 'mdp987', 'utilisateur'),
('G998877665', 'Lopez', 'Marco', '123 avenue de l’Industrie', '1995-09-30', 29, 'marco.lopez@email.com', 'marcoL', 'mdp123', 'utilisateur'),
('H334455667', 'Bernard', 'Isabelle', '456 rue des Lilas', '1982-07-14', 42, 'isabelle.bernard@email.com', 'isabelleB', 'mdp456', 'administrateur'),
('I667788990', 'Roux', 'Jean', '789 rue de la République', '1980-06-02', 44, 'jean.roux@email.com', 'jeanR', 'mdp789', 'utilisateur'),
('J223344556', 'Moreau', 'Emilie', '234 rue des Tilleuls', '1996-11-30', 28, 'emilie.moreau@email.com', 'emilieM', 'mdp321', 'utilisateur');


INSERT INTO participation (idAdherent, idSeance, note) VALUES
('A123456789', 1, 4.5),
('B987654321', 2, 3.8),
('C246810121', 3, 4.2),
('D135791113', 4, 4.7),
('E246802468', 5, 3.5),
('F112233445', 6, 4.1),
('G998877665', 7, 3.9),
('H334455667', 8, 4.6),
('I667788990', 9, 3.7),
('J223344556', 10, 4.3);

-- Vues ------------------------------------------------------------------------------

-- Procédures ------------------------------------------------------------------------------


-- Fonctions ------------------------------------------------------------------------------
DELIMITER //


delimiter ;




