-- Projet final de BD
-- Justin Bélanger & Florence Blackburn

-- RESET ------------------------------------------------------------------------------
DROP TABLE IF EXISTS adherent;

DROP TABLE IF EXISTS participation;
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
    ON UPDATE CASCADE
);

CREATE TABLE seance(
    idSeance INT AUTO_INCREMENT,
    activiteNom VARCHAR(155),
    activiteType VARCHAR(155),
    dateHeure DATETIME,
    nbPlacesDispos INT,
    PRIMARY KEY pk_seance (idSeance),
    FOREIGN KEY fk_seance_activite (activiteNom, activiteType) REFERENCES activite(nom, type)
    ON UPDATE CASCADE
    );

CREATE TABLE adherent(
    noIdentification VARCHAR(11),
    nom VARCHAR(155),
    prenom VARCHAR(155),
    adresse VARCHAR(255),
    dateNaissance DATE,
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
    ON UPDATE CASCADE
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
            YEAR(NEW.dateNaissance),
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
        SET NEW.age = (YEAR(CURDATE())- YEAR(NEW.dateNaissance));
    end //
delimiter ;

-- Permet de gérer le nombre de places disponibles dans chaque séance
DROP TRIGGER IF EXISTS gerer_nbPlaceDispo_seance;
DELIMITER //
CREATE TRIGGER gerer_nbPlaceDispo_seance AFTER INSERT ON participation FOR EACH ROW
    BEGIN
        UPDATE seance
            SET nbPlacesDispos = nbPlacesDispos-1
        WHERE idSeance = NEW.idSeance;
    end //
delimiter ;

/*
 Permet d’insérer les participants dans une séance si le nombre de places maximum n’est pas atteint.
 Sinon, il affiche un message d’erreur avisant qu’il ne reste plus de places disponibles pour la
 séance choisie.
 */
DELIMITER //
CREATE TRIGGER verifier_dispos BEFORE INSERT ON participation FOR EACH ROW
    BEGIN
        DECLARE nbPlacesDisposSeance INT;
    SELECT nbPlacesDispos FROM seance WHERE seance.idSeance = NEW.idSeance INTO nbPlacesDisposSeance;
        IF (nbPlacesDisposSeance<=0) THEN
            SIGNAL SQLSTATE '45000' SET message_text="Il n'y a plus de places pour cette séance.";
        end if ;
    end //
delimiter ;


-- Supprimer tout les enfants d'une activité relier
DELIMITER //
CREATE TRIGGER supprimer_activite
    BEFORE DELETE
    ON activite
    FOR EACH ROW
    BEGIN

        DELETE FROM participation WHERE idSeance  IN(SELECT idSeance FROM seance
                                                            WHERE activiteNom = OLD.nom AND activiteType = OLD.type);

        DELETE FROM seance WHERE activiteNom = OLD.nom AND activiteType = OLD.type;

    end //
DELIMITER ;



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
('Football', 'Sports', 10.00, 50.00),
('Yoga', 'Musculation', 5.00, 30.00),
('Concert', 'Musique', 20.00, 100.00),
('Peinture', 'Art', 10.00, 20.00),
('Pâtisserie', 'Cuisine', 20.00, 40.00),
('Danse', 'Loisirs créatifs', 15.00, 35.00),
('Programmation', 'Technologie', 15.00, 75.00),
('Randonnée', 'Voyages', 5.00, 20.00),
('Méditation', 'Santé', 8.00, 25.00),
('Cours de langue', 'Éducation', 20.00, 45.00);

INSERT INTO seance (activiteNom, activiteType, dateHeure, nbPlacesDispos) VALUES
('Football', 'Sports', '2024-12-01 10:00:00', 20),
('Yoga', 'Musculation', '2024-11-02 08:30:00', 15),
('Concert', 'Musique', '2024-12-03 19:00:00', 100),
('Peinture', 'Art', '2024-11-04 14:00:00', 12),
('Pâtisserie', 'Cuisine', '2024-04-05 16:30:00', 10),
('Danse', 'Loisirs créatifs', '2024-02-06 18:00:00', 25),
('Programmation', 'Technologie', '2024-12-07 09:00:00', 20),
('Randonnée', 'Voyages', '2024-11-08 07:00:00', 30),
('Méditation', 'Santé', '2024-06-09 06:00:00', 15),
('Cours de langue', 'Éducation', '2024-12-10 10:30:00', 18);


INSERT INTO seance (activiteNom, activiteType, dateHeure, nbPlacesDispos)
VALUES
    ('Pâtisserie', 'Cuisine', '2024-12-01 13:00:00', 20),
    ('Pâtisserie', 'Cuisine', '2024-12-03 10:00:00', 20),
    ('Pâtisserie', 'Cuisine', '2024-12-03 13:00:00', 20),
    ('Peinture', 'Art', '2024-11-04 16:00:00', 12),
    ('Peinture', 'Art', '2024-11-04 19:00:00', 12),
    ('Peinture', 'Art', '2024-11-05 12:00:00', 12),
    ('Danse', 'Loisirs créatifs', '2024-02-06 18:00:00', 25),
    ('Danse', 'Loisirs créatifs', '2024-02-06 18:00:00', 18),
    ('Danse', 'Loisirs créatifs', '2024-02-07 15:30:00', 25),
    ('Danse', 'Loisirs créatifs', '2024-02-07 18:00:00', 25),
    ('Danse', 'Loisirs créatifs', '2024-02-08 10:45:00', 25),
    ('Cours de langue', 'Éducation', '2024-11-02 09:30:00', 15),
    ('Cours de langue', 'Éducation', '2024-11-02 10:30:00', 15),
    ('Cours de langue', 'Éducation', '2024-11-09 10:30:00', 15),
    ('Randonnée', 'Voyages', '2024-12-08 07:00:00', 10);


INSERT INTO adherent (noIdentification, nom, prenom, adresse, dateNaissance, email, pseudo, mdp, role) VALUES
('1', 'Dupont', 'Pierre', '123 rue des Fleurs', '1990-05-12',  'pierre.dupont@email.com', 'pierreD', 'mdp123', 'administrateur'),
('2', 'Martin', 'Claire', '456 avenue des Champs', '1985-08-20', 'claire.martin@email.com', 'claireM', 'mdp456', 'utilisateur'),
('3', 'Durand', 'Julien', '789 boulevard Saint-Michel', '2000-01-15',  'julien.durand@email.com', 'julienD', 'mdp789', 'utilisateur'),
('4', 'Lemoine', 'Sophie', '234 rue de la Paix', '1993-11-10',  'sophie.lemoine@email.com', 'sophieL', 'mdp321', 'administrateur'),
('5', 'Girard', 'Thomas', '567 rue du Parc', '1992-04-25',  'thomas.girard@email.com', 'thomasG', 'mdp654', 'utilisateur'),
('6', 'Petit', 'Alice', '890 avenue de la Liberté', '1988-02-12',  'alice.petit@email.com', 'aliceP', 'mdp987', 'utilisateur'),
('7', 'Lopez', 'Marco', '123 avenue de l’Industrie', '1995-09-30',  'marco.lopez@email.com', 'marcoL', 'mdp123', 'utilisateur'),
('8', 'Bernard', 'Isabelle', '456 rue des Lilas', '1982-07-14',  'isabelle.bernard@email.com', 'isabelleB', 'mdp456', 'administrateur'),
('9', 'Roux', 'Jean', '789 rue de la République', '1980-06-02',  'jean.roux@email.com', 'jeanR', 'mdp789', 'utilisateur'),
('10', 'Moreau', 'Emilie', '234 rue des Tilleuls', '1996-11-30',  'emilie.moreau@email.com', 'emilieM', 'mdp321', 'utilisateur');


INSERT INTO participation (idAdherent, idSeance, note) VALUES
('JD-2000-826', 1, 4.5),
('EM-1996-559', 2, 3.8),
('ML-1995-110', 3, 4.2),
('SL-1993-603', 4, 4.7),
('TG-1992-377', 5, 3.5),
('PD-1990-426', 6, 4.1),
('AP-1988-767', 7, 3.9),
('CM-1985-656', 8, 4.6),
('IB-1982-852', 9, 3.7),
('JR-1980-384', 10, 4.3);

INSERT INTO participation (idAdherent, idSeance, note) VALUES
('JD-2000-826', 3, 4.5),
('EM-1996-559', 2, 3.8),
('ML-1995-110', 3, 4.2),
('JD-2000-826', 4, 4.7),
('JD-2000-826', 5, 3.5);

-- Vues ------------------------------------------------------------------------------
--  Trouver le participant ayant le nombre de séances le plus élevé -> Flo
DROP VIEW IF EXISTS adherent_plus_seance;
CREATE VIEW adherent_plus_seance AS
SELECT
    idAdherent
FROM participation
GROUP BY idAdherent
ORDER BY COUNT(idAdherent)  DESC
LIMIT 1;

-- Trouver le prix moyen par activité pour chaque participant
DROP VIEW IF EXISTS moyenne_prix_adhrents;
CREATE VIEW moyenne_prix_adhrents AS
SELECT
    prenom,
    ROUND(AVG(prixVente),2) prix_moyen
FROM adherent
INNER JOIN participation p on adherent.noIdentification = p.idAdherent
INNER JOIN a2024_420335ri_eq4.seance s on p.idSeance = s.idSeance
INNER JOIN a2024_420335ri_eq4.activite a on s.activiteNom = a.nom and s.activiteType = a.type
GROUP BY noIdentification;

--  Afficher les notes d’appréciation pour chaque activité 
DROP VIEW IF EXISTS notes_appreciations_activite;
CREATE VIEW notes_appreciations_activite AS
SELECT
    note,
    nom
FROM participation
INNER JOIN seance s on participation.idSeance = s.idSeance
INNER JOIN activite a on s.activiteNom = a.nom and s.activiteType = a.type
;

--  Affiche la moyenne des notes d’appréciations pour toutes les activités ->Justin
DROP VIEW IF EXISTS moyenne_notes_appreciations_activite;
CREATE VIEW moyenne_notes_appreciations_activite AS
SELECT
    ROUND(AVG(note),2) moyenne_note,
    nom nom_activite
FROM participation
INNER JOIN seance s on participation.idSeance = s.idSeance
INNER JOIN activite a on s.activiteNom = a.nom and s.activiteType = a.type
GROUP BY nom;
;

-- Afficher le nombre de participant pour chaque activité -> Flo
DROP VIEW IF EXISTS nombre_participants_activites;
CREATE VIEW nombre_participants_activites AS
SELECT
    activiteNom,
    activiteType,
    COUNT(activiteNom) AS 'Nombre de participants'
FROM participation p
INNER JOIN seance s on p.idSeance = s.idSeance
INNER JOIN activite a on s.activiteNom = a.nom and s.activiteType = a.type
GROUP BY activiteNom;


--  Afficher le nombre de participant moyen pour chaque mois -> Flo
DROP VIEW IF EXISTS nbParticipants_mois;
CREATE VIEW nbParticipants_mois AS
SELECT
    MONTHNAME(dateHeure) AS 'mois',
    COUNT(*) AS 'nbParticipants'
FROM participation p
INNER JOIN seance s ON p.idSeance = s.idSeance
GROUP BY MONTH(dateHeure)
ORDER BY MONTH(dateHeure);

-- Procédures ------------------------------------------------------------------------------
-- Affiche les informations d'un adhérent selon son idAdherent -> Flo
DROP PROCEDURE IF EXISTS affiche_adherent;
DELIMITER /
CREATE PROCEDURE affiche_adherent (IN _idAdherent VARCHAR(11))
BEGIN
    SELECT *
    FROM adherent
    WHERE noIdentification = _idAdherent;
end /
delimiter ;

-- Ajouter un adherant a l'aide de procédure -> Justin
DROP PROCEDURE IF EXISTS ajouter_adherant;
DELIMITER //
CREATE PROCEDURE ajouter_adherant(IN _nom VARCHAR(155),IN _prenom VARCHAR(155),IN _adresse VARCHAR(255),IN _dateNaissance DATE,IN _age INT, IN _email VARCHAR(255), IN _pseudo VARCHAR(155), IN _mdp VARCHAR(255),IN _role VARCHAR(155) )
BEGIN
    INSERT INTO adherent ( nom, prenom, adresse, dateNaissance, age, email, pseudo, mdp, role) VALUES (_nom, _prenom, _adresse, _dateNaissance, _age, _email, _pseudo, _mdp, _role);
end //
DELIMITER ;

-- Ajouter une séance -> Justin
DROP PROCEDURE IF EXISTS ajouter_seance;
DELIMITER //
CREATE PROCEDURE ajouter_seance( IN _activiteNom VARCHAR(155),IN _activiteType VARCHAR(155),IN _dateHeure DATETIME,IN _nbPlacesDispos INT)
BEGIN
    INSERT INTO seance (activiteNom, activiteType, dateHeure, nbPlacesDispos) VALUES (_activiteNom, _activiteType, _dateHeure, _nbPlacesDispos);
end //
DELIMITER ;

-- ajouter une participation -> Justin
DROP PROCEDURE IF EXISTS ajouter_participation;
DELIMITER //
CREATE PROCEDURE ajouter_participation(IN _idAdherent VARCHAR(11),IN  _idSeance INT,IN  _note DOUBLE)
BEGIN
    INSERT INTO participation (idAdherent, idSeance, note) VALUES (_idAdherent, _idSeance, _note);
end //
DELIMITER ;


-- ajouter une activité -> Justin
DROP PROCEDURE IF EXISTS ajouter_activite;
DELIMITER //
CREATE PROCEDURE ajouter_activite(IN _nom varchar(155), IN _type varchar(155), IN _coutOrganisation double, IN _prixVente double)
BEGIN
    DECLARE CONTINUE HANDLER FOR 1062
    BEGIN
       SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT ='Ce nom d\'activité existe déjà pour cette catégorie';
    end ;
     INSERT INTO activite (nom, type, coutOrganisation, prixVente) VALUES (_nom, _type, _coutOrganisation, _prixVente);
end //
DELIMITER ;




-- Fonctions ------------------------------------------------------------------------------


-- Retourne l'idAdherent si les informations de connexions sont bonnes -> Flo
DROP FUNCTION IF EXISTS retourne_adherent_connexion;
DELIMITER //
CREATE FUNCTION retourne_adherent_connexion(
    _pseudo VARCHAR(155),
    _mdp VARCHAR(155)
)
RETURNS VARCHAR(11)
BEGIN
    DECLARE idAdherent VARCHAR(11);
    SELECT noIdentification
    FROM adherent
    WHERE pseudo = _pseudo AND mdp = _mdp INTO idAdherent;

    RETURN (idAdherent);
end //
delimiter ;




-- Retourne le nombre d'adherent -> Flo
DROP FUNCTION IF EXISTS nombre_total_adherent;
DELIMITER //
CREATE FUNCTION nombre_total_adherent()
RETURNS INT
BEGIN
    DECLARE nb INT;
    SELECT COUNT(*) INTO nb
    FROM adherent;
    RETURN (nb);
end //
delimiter ;

-- Retourne le nombre d'activités
DROP FUNCTION IF EXISTS nombre_total_activite;
DELIMITER //
CREATE FUNCTION nombre_total_activite()
RETURNS INT
BEGIN
    DECLARE nb INT;
    SELECT COUNT(*) INTO nb
    FROM activite;
    RETURN (nb);
end //
delimiter ;

-- Retourne le nombre d'adherent pour une activite particuliere -> Flo
DROP FUNCTION IF EXISTS nombre_total_adherent_selon_activite;
DELIMITER //
CREATE FUNCTION nombre_total_adherent_selon_activite(
    _nomActivite VARCHAR(155),
    _typeActivite VARCHAR(155)
)
RETURNS INT
BEGIN
    DECLARE nb INT;
    SELECT COUNT(*) INTO nb
    FROM participation p
    INNER JOIN seance s on p.idSeance = s.idSeance
    INNER JOIN activite a on s.activiteNom = a.nom and s.activiteType = a.type
    WHERE nom = _nomActivite AND  type = _typeActivite;
    RETURN (nb);
end //
delimiter ;

-- Retourne la moyenne des évaluations pour une activite particuliere -> Flo
DROP FUNCTION IF EXISTS moyenne_evaluation_selon_activite;
DELIMITER //
CREATE FUNCTION moyenne_evaluation_selon_activite(
    _nomActivite VARCHAR(155),
    _typeActivite VARCHAR(155)
)
RETURNS DOUBLE
BEGIN
    DECLARE nb DOUBLE;
    SELECT AVG(note) INTO nb
    FROM participation p
    INNER JOIN seance s on p.idSeance = s.idSeance
    INNER JOIN activite a on s.activiteNom = a.nom and s.activiteType = a.type
    WHERE nom = _nomActivite AND  type = _typeActivite;
    RETURN (nb);
end //
delimiter ;


-- Erreurs ------------------------------------------------------------------------------

-- UNE À LA PROCÉDURE D'AJOUT D'ACTVITÉ


-- Empêcher un participant de s'inscrire deux fois à la même séance. -> Flo
DROP TRIGGER IF EXISTS empecher_participation_doublons;
DELIMITER //
CREATE TRIGGER empecher_participation_doublons BEFORE INSERT ON participation FOR EACH ROW
    BEGIN
        IF ((SELECT idParticipation
        FROM participation
        WHERE idAdherent = NEW.idAdherent AND
        idSeance = NEW.idSeance) IS NOT NULL) THEN
            SIGNAL SQLSTATE '45000' SET message_text="Cet adhérent est déjà incrit à cette séance.";
        end if ;

    end //
delimiter ;



-- Empêcher un adherent d'être moins de 18 ans -> Flo
DROP TRIGGER IF EXISTS adherent_moins_18ans_erreur;
DELIMITER //
CREATE TRIGGER adherent_moins_18ans_erreur BEFORE INSERT ON adherent FOR EACH ROW
    BEGIN
        IF(NEW.age < 18) THEN
            SIGNAL SQLSTATE '45000' SET message_text="Un adherent ne peut pas être mineur.";
        end if ;
    end //
delimiter ;





