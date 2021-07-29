-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 08. Mrz 2021 um 12:40
-- Server-Version: 10.4.17-MariaDB
-- PHP-Version: 8.0.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `gvmp`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `accounts`
--

CREATE TABLE `accounts` (
  `id` int(10) NOT NULL,
  `social` varchar(255) NOT NULL,
  `hwid` varchar(255) NOT NULL,
  `playtime` int(255) NOT NULL DEFAULT 0,
  `warns` int(10) NOT NULL DEFAULT 0,
  `banned` int(10) NOT NULL DEFAULT 0,
  `banreason` varchar(255) NOT NULL DEFAULT 'null',
  `ip` varchar(255) NOT NULL DEFAULT '0.0.0.0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `accounts`
--

INSERT INTO `accounts` (`id`, `social`, `hwid`, `playtime`, `warns`, `banned`, `banreason`, `ip`) VALUES
(2, 'IchRauchHanf', 'D8903A045B5289701232BE0846741D10B9BEACE422084B40884C5B64B702B240A53442388ED277683FE837781C96A8805EB26D1C7BF46E18AA7C2AA887B8F080', 7, 0, 0, 'null', '127.0.0.1');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bank`
--

CREATE TABLE `bank` (
  `id` int(20) NOT NULL,
  `name` varchar(255) NOT NULL,
  `tresor` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `bank`
--

INSERT INTO `bank` (`id`, `name`, `tresor`) VALUES
(1, 'Staatsbank', '[{\"Id\":1,\"Slot\":0,\"Weight\":0,\"Name\":\"Goldbarren\",\"ImagePath\":\"goldbarren.png\",\"Amount\":48},{\"Id\":1,\"Slot\":1,\"Weight\":0,\"Name\":\"Banknoten\",\"ImagePath\":\"Notizblock.png\",\"Amount\":14}]'),
(1, 'Staatsbank', '[{\"Id\":1,\"Slot\":0,\"Weight\":0,\"Name\":\"Goldbarren\",\"ImagePath\":\"goldbarren.png\",\"Amount\":48},{\"Id\":1,\"Slot\":1,\"Weight\":0,\"Name\":\"Banknoten\",\"ImagePath\":\"Notizblock.png\",\"Amount\":14}]');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `business`
--

CREATE TABLE `business` (
  `id` int(11) NOT NULL,
  `name` varchar(150) NOT NULL,
  `motd` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `business`
--

INSERT INTO `business` (`id`, `name`, `motd`) VALUES
(1, 'Test', 'HALLO'),
(1, 'Test', 'HALLO');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `clothingsdata`
--

CREATE TABLE `clothingsdata` (
  `name` varchar(255) NOT NULL,
  `category` varchar(255) NOT NULL,
  `component` int(100) NOT NULL,
  `drawable` int(100) NOT NULL,
  `texture` int(100) NOT NULL,
  `id` int(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `clothingsdata`
--

INSERT INTO `clothingsdata` (`name`, `category`, `component`, `drawable`, `texture`, `id`) VALUES
('Standart Sneaker', 'Schuhe', 6, 1, 0, 1),
('Schlappen', 'Schuhe', 6, 6, 0, 2),
('Weiße Schuhe', 'Schuhe', 6, 7, 0, 3),
('Schwarze Schuhe', 'Schuhe', 6, 7, 2, 4),
('Lilane Schuhe', 'Schuhe', 6, 7, 14, 5),
('Blaue Schuhe', 'Schuhe', 6, 7, 5, 6),
('Anzugsschuhe', 'Schuhe', 6, 10, 0, 7),
('Arbeitsschuhe', 'Schuhe', 6, 25, 0, 8),
('Gelb-Schwarze Air Max', 'Schuhe', 6, 31, 1, 9),
('Weiße Air Max', 'Schuhe', 6, 31, 2, 10),
('Schluppen', 'Schuhe', 6, 42, 0, 11),
('Bunte Schuhe', 'Schuhe', 6, 58, 0, 12),
('Blaue Leuchtschuhe', 'Schuhe', 6, 77, 0, 13),
('Grüne Leuchtschuhe', 'Schuhe', 6, 77, 5, 14),
('Rote Leuchtschuhe', 'Schuhe', 6, 77, 3, 15),
('Grüne Schluppen', 'Schuhe', 6, 92, 0, 16),
('Gelbe Schluppen', 'Schuhe', 6, 95, 0, 17),
('Affenmaske', 'Maske', 1, 3, 0, 18),
('Weihnachtsmann', 'Maske', 1, 8, 0, 19),
('Waschbärmaske', 'Maske', 1, 20, 0, 20),
('Bärmaske', 'Maske', 1, 21, 0, 21),
('Falkenmaske', 'Maske', 1, 24, 0, 22),
('Schwarzes Bandana', 'Maske', 1, 51, 0, 23),
('Grünes Bandana', 'Maske', 1, 51, 5, 24),
('Lilanes Bandana', 'Maske', 1, 51, 6, 25),
('Gelbes Bandana', 'Maske', 1, 51, 8, 26),
('Schwarze Vollverschleierungsmaske', 'Maske', 1, 54, 0, 27),
('Weiße Vollverschleierungsmaske', 'Maske', 1, 54, 1, 28),
('Schwarze Überfallsmaske', 'Maske', 1, 57, 0, 29),
('Teufelsmaske', 'Maske', 1, 68, 0, 30),
('Clownsmaske', 'Maske', 1, 95, 0, 31),
('Pferdemaske', 'Maske', 1, 97, 0, 32),
('Bunte Maske', 'Maske', 1, 102, 0, 33),
('Weißes Bandana mit Muster', 'Maske', 1, 111, 15, 34),
('Schwarzes Bandana mit Muster', 'Maske', 1, 111, 17, 35),
('Kopftuch', 'Maske', 1, 115, 0, 36),
('Mund-Nasen-Schutz', 'Maske', 1, 116, 0, 37),
('MG Maske', 'Maske', 1, 118, 19, 38),
('Schwarze Gasmaske', 'Maske', 1, 129, 0, 39),
('Gelbe Gasmaske', 'Maske', 1, 129, 12, 40),
('Semih', 'Maske', 1, 139, 0, 41),
('Schwarze Mütze', 'Hüte', 1, 2, 0, 42),
('Schwarzer Führerhut', 'Hüte', 1, 7, 2, 44),
('Weißer Führerhut', 'Hüte', 1, 7, 0, 45),
('Normaler Hut', 'Hüte', 1, 12, 0, 46),
('Zylinderkopf', 'Hüte', 1, 27, 0, 47),
('Schwarze Mütze', 'Hüte', 1, 58, 2, 48),
('Anführerhut', 'Hüte', 1, 113, 0, 49),
('Schwarze Diamondmütze nach hinten', 'Hüte', 1, 136, 1, 50),
('Weiße Diamondmütze nach hinten', 'Hüte', 1, 136, 2, 51),
('Weiße Diamondmütze nach vorne', 'Hüte', 1, 135, 2, 52),
('Schwarze Diamondmütze nach vorne', 'Hüte', 1, 135, 1, 53),
('Rentierhut', 'Hüte', 1, 101, 0, 54),
('Weihnachtshut', 'Hüte', 1, 100, 0, 55),
('Jogger Weiß', 'Hose', 4, 5, 0, 56),
('Jogger Schwarz', 'Hose', 4, 5, 2, 57),
('Jogger Blau', 'Hose', 4, 5, 3, 58),
('Jogger Rot', 'Hose', 4, 5, 5, 59),
('Jogger Grün', 'Hose', 4, 5, 6, 60),
('Jogger Orange', 'Hose', 4, 5, 7, 61),
('Jogger Gelb', 'Hose', 4, 5, 8, 62),
('Jogger Lila', 'Hose', 4, 5, 9, 63),
('Schwarze kurze Hose', 'Hose', 4, 12, 0, 64),
('Rote Badehose', 'Hose', 4, 16, 1, 65),
('Grüne Badehose', 'Hose', 4, 16, 6, 66),
('Türkise Badehose', 'Hose', 4, 16, 8, 67),
('Gelbe kurze Hose', 'Hose', 4, 17, 2, 68),
('Blaue kurze Hose', 'Hose', 4, 17, 4, 69),
('Schwarze Anzugshose', 'Hose', 4, 24, 0, 70),
('Weiße Anzugshose', 'Hose', 4, 24, 5, 71),
('Schwarze Jeans', 'Hose', 4, 26, 0, 72),
('Schwarze Cargohose', 'Hose', 4, 31, 0, 73),
('Schwarze Gangsterhose', 'Hose', 4, 42, 0, 74),
('Weiße Gangsterhose', 'Hose', 4, 42, 2, 75),
('Gelbe Gangsterhose', 'Hose', 4, 42, 5, 76),
('Grüne Gangsterhose', 'Hose', 4, 42, 6, 77),
('Weißer Bademantel', 'Hose', 4, 119, 4, 78),
('Schwarzer Bademantel', 'Hose', 4, 119, 2, 79),
('Bunte Hose', 'Hose', 4, 85, 0, 80),
('Normaler Körper', 'Koerper', 3, 0, 0, 81),
('Körper 1', 'Koerper', 3, 1, 0, 82),
('Körper 2', 'Koerper', 3, 2, 0, 83),
('Körper 4', 'Koerper', 3, 4, 0, 84),
('Körper 5', 'Koerper', 3, 5, 0, 85),
('Körper 6', 'Koerper', 3, 6, 0, 86),
('Körper 8', 'Koerper', 3, 8, 0, 87),
('Körper 11', 'Koerper', 3, 11, 0, 88),
('Körper 12', 'Koerper', 3, 12, 0, 89),
('Körper 14', 'Koerper', 3, 14, 0, 90),
('Körper 15', 'Koerper', 3, 15, 0, 91),
('Kein Unterteil', 'Unterteil', 8, 15, 0, 92),
('Weißes TShirt Unterteil', 'Unterteil', 8, 0, 0, 93),
('Schwarzes TShirt Unterteil', 'Unterteil', 8, 0, 2, 94),
('Tank Top Unterteil', 'Unterteil', 8, 5, 0, 98),
('Weißes Anzugsunterteil', 'Unterteil', 8, 10, 0, 99),
('Schwarzes Anzugsunterteil', 'Unterteil', 8, 10, 2, 100),
('Weißes T-Shirt', 'Oberteil', 11, 0, 0, 101),
('Schwarzes T-Shirt', 'Oberteil', 11, 0, 2, 102),
('Jogger Oberteil Weiß', 'Oberteil', 11, 3, 0, 103),
('Jogger Oberteil Schwarz', 'Oberteil', 11, 3, 2, 104),
('Tank Top Oberteil', 'Oberteil', 11, 5, 0, 105),
('Schwarzer Fischerhut', 'Hüte', 1, 3, 1, 106),
('Schwarz-Weiß gestreiftes Oberteil', 'Oberteil', 11, 14, 7, 107),
('Schwarzes Cargooberteil', 'Oberteil', 11, 53, 0, 108),
('Schwarzer Luxusmantel', 'Oberteil', 11, 70, 9, 109),
('Weißer Luxusmantel', 'Oberteil', 11, 70, 4, 110),
('Grauer Mantel', 'Oberteil', 11, 72, 1, 111),
('Bomberjacke auf', 'Oberteil', 11, 74, 0, 112),
('Bomberjacke zu', 'Oberteil', 11, 75, 0, 113),
('Weißes Long T', 'Oberteil', 11, 80, 0, 114),
('Schwarzes Long T', 'Oberteil', 11, 80, 1, 115),
('Schwarzer Pulli', 'Oberteil', 11, 89, 0, 116),
('Schwarzer Pulli', 'Oberteil', 11, 89, 2, 117),
('Blauer Hoodie', 'Oberteil', 11, 96, 0, 118),
('Grauer Kragenpulli', 'Oberteil', 11, 111, 0, 119),
('Roter Kragenpulli', 'Oberteil', 11, 111, 1, 120),
('Schwarzer Kragenpulli', 'Oberteil', 11, 111, 3, 121),
('Grünes Hemd zu', 'Oberteil', 11, 126, 9, 122),
('Rotes Hemd zu', 'Oberteil', 11, 126, 10, 123),
('Schwarzes Hemd zu', 'Oberteil', 11, 126, 11, 124),
('Lila Hemd zu', 'Oberteil', 11, 126, 13, 125),
('Grünes Hemd auf', 'Oberteil', 11, 127, 9, 126),
('Rotes Hemd auf', 'Oberteil', 11, 127, 10, 127),
('Schwarzes Hemd auf', 'Oberteil', 11, 127, 11, 128),
('Lila Hemd auf', 'Oberteil', 11, 127, 13, 129),
('187 Hemd', 'Oberteil', 11, 135, 0, 130),
('Blaue Jacke auf', 'Oberteil', 11, 136, 2, 131),
('Schwarze Jacke auf', 'Oberteil', 11, 136, 6, 132),
('Grüne Jacke zu', 'Oberteil', 11, 143, 0, 133),
('Lila Jacke zu', 'Oberteil', 11, 143, 2, 134),
('Schwarze Jacke zu', 'Oberteil', 11, 143, 6, 135),
('Rote Winterjacke', 'Oberteil', 11, 167, 0, 136),
('Schwarze Winterjacke', 'Oberteil', 11, 167, 3, 137),
('Weiße Winterjacke', 'Oberteil', 11, 167, 14, 138),
('Buntes Oberteil', 'Oberteil', 11, 201, 0, 139),
('Schwarze Sportjacke', 'Oberteil', 11, 224, 0, 140),
('Weiße Sportjacke', 'Oberteil', 11, 224, 2, 141),
('Grüne Bomberjacke', 'Oberteil', 11, 229, 8, 142),
('Schwarze Bomberjacke', 'Oberteil', 11, 229, 2, 143),
('Weiße Bomberjacke', 'Oberteil', 11, 229, 4, 144),
('Lilane Bomberjacke', 'Oberteil', 11, 229, 10, 145),
('All-in-One Jacke', 'Oberteil', 11, 248, 0, 146),
('Schwarzer Windbreaker', 'Oberteil', 11, 251, 1, 147),
('Weißer Windbreaker', 'Oberteil', 11, 251, 2, 148),
('Blauer Windbreaker', 'Oberteil', 11, 251, 9, 149),
('Grüner Windbreaker', 'Oberteil', 11, 251, 18, 150),
('Dunkelblauer Windbreaker', 'Oberteil', 11, 251, 19, 151),
('Blaues Hemd', 'Oberteil', 11, 260, 10, 152),
('Schwarzes Hemd', 'Oberteil', 11, 260, 11, 153),
('Rotes Hemd', 'Oberteil', 11, 260, 12, 154),
('Schwarz Windbreaker ohne Kapuze', 'Oberteil', 11, 296, 12, 155),
('Lila Windbreaker ohne Kapuze', 'Oberteil', 11, 296, 13, 156),
('Gelb Windbreaker ohne Kapuze', 'Oberteil', 11, 296, 14, 157),
('Gelb Windbreaker mit Kapuze', 'Oberteil', 11, 297, 14, 158),
('Lila Windbreaker mit Kapuze', 'Oberteil', 11, 297, 13, 159),
('Schwarzer Windbreaker mit Kapuze', 'Oberteil', 11, 297, 12, 160),
('Körper 16', 'Koerper', 3, 7, 0, 161),
('Weißer Diamond Hoodie ohne Mütze', 'Oberteil', 11, 305, 2, 162),
('Schwarzer Diamond Hoodie ohne Mütze', 'Oberteil', 11, 305, 3, 163),
('Weißer Diamond Hoodie mit Mütze', 'Oberteil', 11, 306, 2, 164),
('Schwarzer Diamond Hoodie mit Mütze', 'Oberteil', 11, 306, 3, 165),
('Nils Hose', 'Hose', 4, 132, 0, 166),
('Semihs Beine', 'Hose', 4, 127, 0, 167),
('Braune Hose', 'Hose', 4, 47, 0, 168),
('Brauen Cargo Hose', 'Hose', 4, 46, 0, 169),
('Schwarze Cargo Hose', 'Hose', 4, 34, 0, 170),
('Braune Kurze Hose', 'Hose', 4, 15, 0, 171),
('Semihs Arme', 'Koerper', 3, 169, 0, 172),
('Semihs Oberteil', 'Oberteil', 11, 333, 0, 173),
('Lost Oberteil 2', 'Oberteil', 11, 174, 0, 174),
('Lost Oberteil mit Arm', 'Oberteil', 11, 172, 1, 175),
('Lost Oberteil ohne Arm', 'Oberteil', 11, 162, 0, 176),
('Lost Oberteil kp', 'Oberteil', 11, 161, 0, 177),
('Schwarzes Lost Oberteil', 'Oberteil', 11, 160, 0, 178),
('Oberteil ', 'Oberteil', 11, 165, 0, 179),
('Oberteil 2', 'Oberteil', 11, 165, 2, 180),
('Oberteil 1', 'Oberteil', 11, 165, 1, 181),
('Schwarzes Oberteil mit Kaputze', 'Oberteil', 11, 202, 0, 182),
('Schwarzes Oberteil ohne Kaputze', 'Oberteil', 11, 205, 0, 183),
('Schwarze Jacke 1', 'Oberteil', 11, 248, 0, 184),
('Roter Diamond Hoodie mit Mütze', 'Oberteil', 11, 306, 4, 185),
('Roter Diamond Hoodie ohne Mütze', 'Oberteil', 11, 305, 4, 186);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fraktionen`
--

CREATE TABLE `fraktionen` (
  `fraktionId` int(10) NOT NULL,
  `fraktionName` varchar(255) NOT NULL,
  `shortName` varchar(100) NOT NULL,
  `spawnPoint` varchar(150) NOT NULL,
  `fraktionsLagerPoint` varchar(150) NOT NULL,
  `frakLaborPoint` varchar(150) NOT NULL,
  `fraktionsDimension` int(10) NOT NULL,
  `garagePoint` varchar(150) NOT NULL,
  `garageSpawnPoint` varchar(150) NOT NULL,
  `garageSpawnPointRotation` double NOT NULL,
  `blipColor` int(30) NOT NULL,
  `rgbColor` varchar(150) NOT NULL,
  `fraktionsClothes` varchar(1000) NOT NULL,
  `isBadFraktion` int(10) NOT NULL DEFAULT 1,
  `frakKontostand` int(50) NOT NULL DEFAULT 0,
  `fraklager` varchar(500) NOT NULL DEFAULT '[]'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `fraktionen`
--

INSERT INTO `fraktionen` (`fraktionId`, `fraktionName`, `shortName`, `spawnPoint`, `fraktionsLagerPoint`, `frakLaborPoint`, `fraktionsDimension`, `garagePoint`, `garageSpawnPoint`, `garageSpawnPointRotation`, `blipColor`, `rgbColor`, `fraktionsClothes`, `isBadFraktion`, `frakKontostand`, `fraklager`) VALUES
(1, 'Los Santos Vagos', 'Vagos', '{}', '{\"x\":-1088.615,\"y\":-1622.645,\"z\":3.632734}', '{\"x\":-1120.434,\"y\":-1624.769,\"z\":3.29842}', 10, '{}', '{}', 0, 46, '{\"Red\":224,\"Green\":194,\"Blue\":0,\"Alpha\":255}', '[{\"name\":\"Vagos Maske\",\"category\":\"Maske\",\"component\":1,\"drawable\":51,\"texture\":8},{\"name\":\"Vagos Oberteil\",\"category\":\"Oberteil\",\"component\":11,\"drawable\":111,\"texture\":3},{\"name\":\"Keine\",\"category\":\"Unterteil\",\"component\":8,\"drawable\":15,\"texture\":0},{\"name\":\"Körper 1\",\"category\":\"Körper\",\"component\":3,\"drawable\":4,\"texture\":0},{\"name\":\"Vagos Hose\",\"category\":\"Hose\",\"component\":4,\"drawable\":5,\"texture\":8},{\"name\":\"Vagos Schuhe\",\"category\":\"Schuhe\",\"component\":6,\"drawable\":6,\"texture\":0}]', 1, 0, '[]'),
(2, 'Marabunta Grande 13', 'MG13', '{}', '{\"x\":1297.29,\"y\":-1610.01,\"z\":57.34}', '{\"x\":1744.125,\"y\":-1623.014,\"z\":111.5374}', 11, '{\"x\":1161.16,\"y\":-1643.96,\"z\":35.83424}', '{\"x\":1156.93,\"y\":-1657.76,\"z\":36.6}', 200.36, 3, '{\"Red\":0,\"Green\":140,\"Blue\":255,\"Alpha\":255}', '[{\"name\":\"MG13 Maske\",\"category\":\"Maske\",\"component\":1,\"drawable\":118,\"texture\":19},{\"name\":\"MG13 Clown-Maske\",\"category\":\"Maske\",\"component\":1,\"drawable\":95,\"texture\":0},{\"name\":\"MG13 Oberteil\",\"category\":\"Oberteil\",\"component\":11,\"drawable\":96,\"texture\":0},{\"name\":\"MG13 Oberteil 2\",\"category\":\"Oberteil\",\"component\":11,\"drawable\":305,\"texture\":0},{\"name\":\"Keine\",\"category\":\"Unterteil\",\"component\":8,\"drawable\":15,\"texture\":0},{\"name\":\"Körper 1\",\"category\":\"Körper\",\"component\":3,\"drawable\":4,\"texture\":0},{\"name\":\"Mg13 Hose\",\"category\":\"Hose\",\"component\":4,\"drawable\":5,\"texture\":3},{\"name\":\"Mg13 Hose 2\",\"category\":\"Hose\",\"component\":4,\"drawable\":16,\"texture\":8},{\"name\":\"MG13 Schuhe\",\"category\":\"Schuhe\",\"component\":6,\"drawable\":7,\"texture\":0}]', 1, 0, '[]'),
(3, 'Los Santos Police Department', 'LSPD', '{}', '{}', '{}', 12, '{\"x\":457.9414,\"y\":-1010.78,\"z\":27.19259}', '{\"x\":445.4714,\"y\":-991.4546,\"z\":24.5998}', 267.1716, 3, '{\"Red\":0,\"Green\":0,\"Blue\":255,\"Alpha\":255}', '[]', 0, 1000000, '[{\"Id\":1,\"Slot\":0,\"Weight\":0,\"Name\":\"Advancedrifle\",\"ImagePath\":\"AdvanvcedRifle.png\",\"Amount\":1},{\"Id\":1,\"Slot\":1,\"Weight\":0,\"Name\":\"Angel\",\"ImagePath\":\"angel.png\",\"Amount\":1},{\"Id\":1,\"Slot\":2,\"Weight\":0,\"Name\":\"Hamburger\",\"ImagePath\":\"hamburger.png\",\"Amount\":1},{\"Id\":1,\"Slot\":3,\"Weight\":0,\"Name\":\"Gusenberg\",\"ImagePath\":\"Gusenberg.png\",\"Amount\":2},{\"Id\":1,\"Slot\":4,\"Weight\":0,\"Name\":\"Bier\",\"ImagePath\":\"beer.png\",\"Amount\":1}]'),
(4, 'Los Santos Medical Department', 'LSMC', '{}', '{}', '{}', 13, '{\"x\":302.0835,\"y\":-602.6228,\"z\":42.31903}', '{\"x\":296.9345,\"y\":-607.9948,\"z\":42.16302}', 70.56174, 3, '{\"Red\":255,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]', 0, 1000000, '[]'),
(5, 'Federal Investigation Bureau', 'FIB', '{}', '{}', '{}', 14, '{\"x\":95.5163,\"y\":-723.778,\"z\":32.03326}', '{\"x\":110.002,\"y\":-715.3648,\"z\":32.03326}', 157.3429, 0, '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]', 0, 1000000, '[]'),
(6, 'Los Santos Military Police', 'Army', '{}', '{}', '{}', 14, '{\"x\":-2431.358,\"y\":3309.099,\"z\":31.87779}', '{\"x\":-2404.936,\"y\":3287.394,\"z\":31.73013}', 57.04811, 56, '{\"Red\":255,\"Green\":153,\"Blue\":51,\"Alpha\":255}', '[]', 0, 1000000, '[]'),
(7, 'La Cosa Nostra', 'LCN', '{}', '{\"x\":-1541.306,\"y\":91.9437,\"z\":52.79737}', '{\"x\":-2312.065,\"y\":326.7191,\"z\":168.6967}', 16, '{}', '{}', 0, 85, '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]', 1, 0, '[]'),
(8, 'Front Yard Ballas', 'Ballas', '{}', '{\"x\":105.013,\"y\":-1976.551,\"z\":19.86496}', '{\"x\":-58.2678,\"y\":-2244.775,\"z\":7.851522}', 17, '{}', '{}', 0, 7, '{\"Red\":76,\"Green\":0,\"Blue\":153,\"Alpha\":255}', '[]', 1, 0, '[]'),
(9, 'Midnight', 'Midnight', '{}', '{\"x\":-329.2941,\"y\":-2715.39,\"z\":10.39457}', '{\"x\":1197.156,\"y\":-3253.645,\"z\":5.995179}', 18, '{}', '{}', 0, 0, '{\"Red\":255,\"Green\":255,\"Blue\":255,\"Alpha\":255}', '[]', 1, 0, '[]'),
(10, 'High Rollin Hustlers', 'Hustlers', '{}', '{\"x\":-948.413,\"y\":214.6389,\"z\":66.33995}', '{\"x\":-372.2683,\"y\":194.2179,\"z\":82.96224}', 19, '{}', '{}', 0, 0, '{\"Red\":255,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]', 1, 0, '[]'),
(11, 'Lost MC', 'Lost MC', '{}', '{\"x\":1994.765,\"y\":3046.313,\"z\":46.11523}', '{\"x\":2403.542,\"y\":3127.776,\"z\":47.05293}', 20, '{}', '{}', 0, 0, '{\"Red\":51,\"Green\":25,\"Blue\":0,\"Alpha\":255}', '[]', 1, 0, '[]'),
(12, 'Devilish Kartell', 'Devilish', '{}', '{\"x\":-800.778,\"y\":174.7714,\"z\":75.64081}', '{\"x\":1094.854,\"y\":-265.6883,\"z\":68.21383}', 21, '{}', '{}', 0, 0, '{\"Red\":255,\"Green\":128,\"Blue\":0,\"Alpha\":255}', '[]', 1, 0, '[]');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fraktion_vehicles`
--

CREATE TABLE `fraktion_vehicles` (
  `id` int(100) NOT NULL,
  `fraktionName` varchar(255) NOT NULL,
  `vehicleModel` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `fraktion_vehicles`
--

INSERT INTO `fraktion_vehicles` (`id`, `fraktionName`, `vehicleModel`) VALUES
(1, 'Los Santos Police Department', 'police3'),
(2, 'Los Santos Police Department', 'police2'),
(3, 'Los Santos Police Department', 'police4'),
(4, 'Los Santos Police Department', 'riot'),
(5, 'Los Santos Police Department', 'policeb'),
(6, 'Los Santos Medical Department', 'ambulance'),
(7, 'Federal Investigation Bureau', 'fbi'),
(8, 'Federal Investigation Bureau', 'fbi2'),
(9, 'Federal Investigation Bureau', 'police4'),
(10, 'Federal Investigation Bureau', 'riot'),
(11, 'Los Santos Military Police', 'rhino'),
(12, 'Los Santos Military Police', 'barracks3'),
(13, 'Los Santos Military Police', 'barracks2'),
(14, 'Los Santos Military Police', 'barracks'),
(15, 'Los Santos Military Police', 'crusader'),
(16, 'Los Santos Military Police', 'insurgent');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gebiete`
--

CREATE TABLE `gebiete` (
  `id` int(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `fraktion` varchar(255) NOT NULL,
  `position` varchar(255) NOT NULL,
  `gebietRadius` double NOT NULL,
  `flagOne` varchar(255) NOT NULL,
  `flagTwo` varchar(255) NOT NULL,
  `flagThree` varchar(255) NOT NULL,
  `flagFour` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `gebiete`
--

INSERT INTO `gebiete` (`id`, `name`, `fraktion`, `position`, `gebietRadius`, `flagOne`, `flagTwo`, `flagThree`, `flagFour`) VALUES
(1, 'Film Studios', 'FIB', '{\"x\":-1153.069,\"y\":-524.53,\"z\":31.150}', 300, '{\"x\":-1203.7606,\"y\":-573.2084,\"z\":26.127436}', '{\"x\":-1187.7123,\"y\":-501.3939,\"z\":35.318523}', '{\"x\":-1098.9534,\"y\":-457.54355,\"z\":34.268784}', '{\"x\":-1012.28217,\"y\":-480.60526,\"z\":38.870663}'),
(2, 'Schmelzerei', 'FIB', '{\"x\":1051.3174,\"y\":-1980.34,\"z\":29.914}', 250, '{\"x\":1054.77,\"y\":-2004.87,\"z\":29.925}', '{\"x\":1044.30,\"y\":-1957.78,\"z\":34.03}', '{\"x\":1088.4913,\"y\":-1914.9285,\"z\":34.16}', '{\"x\":1131.71,\"y\":-2047.55,\"z\":29.91}'),
(3, 'Theater', 'Triaden', '{\"x\":685.40,\"y\":567.53,\"z\":129.36}', 300, '{\"x\":607.98755,\"y\":530.88434,\"z\":135.54}', '{\"x\":686.83,\"y\":595.42,\"z\":139.68}', '{\"x\":738.66,\"y\":564.86,\"z\":124.81}', '{\"x\":614.9856,\"y\":626.67,\"z\":128}'),
(4, 'Pacific Bluffs', 'Triaden', '{\"x\":-2023.0957,\"y\":-340.989,\"z\":47.006}', 385, '{\"x\":-2035.94,\"y\":-367.58,\"z\":47}', '{\"x\":-2204.72,\"y\":-371.15,\"z\":12.19}', '{\"x\":-2132.96,\"y\":-396.47,\"z\":12.07}', '{\"x\":-2187.53,\"y\":-410.61,\"z\":12.02}'),
(5, 'Skaterpark', 'Los Santos Vagos', '{\"x\":-925.47,\"y\":-742.29,\"z\":18.82}', 270, '{\"x\":-872.01,\"y\":-675.51,\"z\":26.7}', '{\"x\":-973.671,\"y\":-702.77,\"z\":21.83}', '{\"x\":-990.32,\"y\":-753.69,\"z\":19.83}', '{\"x\":-939.6855,\"y\":-791.3363,\"z\":14.85}'),
(6, 'Footballpark', 'FIB', '{\"x\":813.0073,\"y\":-281.09,\"z\":65.36}', 300, '{\"x\":706.62,\"y\":-302.56,\"z\":58.14}', '{\"x\":770.33,\"y\":-233.91,\"z\":65.01}', '{\"x\":882.17,\"y\":-280.35,\"z\":65.30}', '{\"x\":839.39,\"y\":-257.18,\"z\":64.53}'),
(7, 'Schrottplatz', 'Triaden', '{\"x\":2395.74,\"y\":3093.51,\"z\":47.086}', 280, '{\"x\":2350.525,\"y\":3135.17,\"z\":47.108}', '{\"x\":2352.52,\"y\":3074.54,\"z\":47.052}', '{\"x\":2414.37,\"y\":3138.99,\"z\":47.075}', '{\"x\":2370.9,\"y\":3159.74,\"z\":47.108}');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `settings`
--

CREATE TABLE `settings` (
  `id` int(222) NOT NULL,
  `maintenance` int(10) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `settings`
--

INSERT INTO `settings` (`id`, `maintenance`) VALUES
(1, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `sms`
--

CREATE TABLE `sms` (
  `id` int(20) NOT NULL,
  `sender` varchar(100) NOT NULL,
  `reciver` varchar(100) NOT NULL,
  `message` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `sms`
--

INSERT INTO `sms` (`id`, `sender`, `reciver`, `message`) VALUES
(498994, 'Marco_Alonso', 'Marco_Alonso', '112233'),
(4317174, 'Marco_Alonso', 'Marco_Alonso', '1222'),
(991871, 'Marco_Alonso', 'Marco_Alonso', '1222'),
(3137792, 'Marco_Alonso', 'Marco_Alonso', '22211'),
(1508587, 'Marco_Alonso', 'Marco_Alonso', '22211'),
(6009739, 'Marco_Alonso', 'Marco_Alonso', '1221'),
(4096345, 'Marco_Alonso', 'Marco_Alonso', '122'),
(7696266, 'Marco_Alonso', 'Marco_Alonso', '1122'),
(3393029, 'Marco_Alonso', 'Marco_Alonso', '1122'),
(1105752, 'Marco_Alonso', 'Marco_Alonso', '122'),
(2467259, 'Marco_Alonso', 'Marco_Alonso', '122'),
(0, '38694', '38694', '�Hallo'),
(0, '2211', '2211', 'Hallo'),
(0, '3121', '3121', 'asdaw'),
(0, '3121', '3121', 'ss');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `users`
--

CREATE TABLE `users` (
  `id` int(222) NOT NULL,
  `social` varchar(250) NOT NULL,
  `name` varchar(50) NOT NULL,
  `password` varchar(1000) NOT NULL,
  `location` varchar(300) NOT NULL DEFAULT '{"x":-1037.739,"y":-2737.8374,"z":20.569271}',
  `rank` varchar(100) NOT NULL DEFAULT 'User',
  `fraktion` varchar(100) NOT NULL DEFAULT 'Zivilist',
  `fraktionRank` int(10) NOT NULL DEFAULT 0,
  `business` varchar(100) NOT NULL,
  `businessRank` int(10) NOT NULL,
  `spindId` int(20) NOT NULL,
  `houseId` int(255) NOT NULL,
  `money` int(200) NOT NULL DEFAULT 750000,
  `blackmoney` int(200) NOT NULL,
  `bank` int(200) NOT NULL,
  `customization` varchar(1000) NOT NULL DEFAULT '[]',
  `clothes` varchar(500) NOT NULL DEFAULT '[]',
  `death` int(2) NOT NULL DEFAULT 0,
  `hwid` varchar(2000) NOT NULL,
  `loadout` varchar(255) NOT NULL DEFAULT '[]',
  `jailtime` int(10) NOT NULL,
  `spind` varchar(500) NOT NULL,
  `phonenumber` int(7) NOT NULL,
  `job` varchar(155) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `users`
--

INSERT INTO `users` (`id`, `social`, `name`, `password`, `location`, `rank`, `fraktion`, `fraktionRank`, `business`, `businessRank`, `spindId`, `houseId`, `money`, `blackmoney`, `bank`, `customization`, `clothes`, `death`, `hwid`, `loadout`, `jailtime`, `spind`, `phonenumber`, `job`) VALUES
(5, 'IchRauchHanf', 'Marco_Alonso', '$2a$12$.eGhxPzwkudzF8i7XKWS3eRtXYxN6EP8qgW.5tsiQsg7ngP1TP/wu', '{\"x\":-25.3354645,\"y\":-1124.4458,\"z\":26.8135281}', 'Projektleitung', 'Los Santos Police Department', 12, 'Sip', 12, 0, 1, 9899378, 122082434, 0, '{\"customization\":{\"Gender\":0,\"Parents\":{\"FatherShape\":0,\"MotherShape\":0,\"FatherSkin\":0,\"MotherSkin\":0,\"Similarity\":1,\"SkinSimilarity\":1},\"Features\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"Hair\":{\"Hair\":8,\"Color\":0,\"HighlightColor\":0},\"Appearance\":[{\"Value\":255,\"Opacity\":1},{\"Value\":0,\"Opacity\":0},{\"Value\":1,\"Opacity\":1},{\"Value\":18,\"Opacity\":0.4},{\"Value\":0,\"Opacity\":0},{\"Value\":0,\"Opacity\":0},{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1},{\"Value\":0,\"Opacity\":0},{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1}],\"EyebrowColor\":0,\"BeardColor\":0,\"EyeColor\":0,\"BlushColor\":0,\"LipstickColor\":0,\"ChestHairColor\":0},\"level\":0}', '{\"API\":{\"Exported\":{}},\"Hut\":{\"API\":{\"Exported\":{}},\"drawable\":-1,\"texture\":0},\"Maske\":{\"API\":{\"Exported\":{}},\"drawable\":57,\"texture\":0},\"Haare\":{\"API\":{\"Exported\":{}},\"drawable\":8,\"texture\":0},\"Oberteil\":{\"API\":{\"Exported\":{}},\"drawable\":53,\"texture\":0},\"Unterteil\":{\"API\":{\"Exported\":{}},\"drawable\":15,\"texture\":0},\"Koerper\":{\"API\":{\"Exported\":{}},\"drawable\":0,\"texture\":0},\"Hose\":{\"API\":{\"Exported\":{}},\"drawable\":31,\"texture\":0},\"Schuhe\":{\"API\":{\"Exported\":{}},\"drawable\":25,\"texture\":0}}', 0, 'D8903A045B5289701232BE0846741D10B9BEACE422084B40884C5B64B702B240A53442388ED277683FE837781C96A8805EB26D1C7BF46E18AA7C2AA887B8F080', '[2937143193,453432689,3523564046]', 0, '[]', 38694, 'LKWFahrer');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user_contacts`
--

CREATE TABLE `user_contacts` (
  `id` int(20) NOT NULL,
  `name` varchar(100) NOT NULL,
  `number` int(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user_inventorys`
--

CREATE TABLE `user_inventorys` (
  `id` int(50) NOT NULL,
  `name` varchar(255) NOT NULL,
  `items` varchar(500) NOT NULL DEFAULT '[]'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `user_inventorys`
--

INSERT INTO `user_inventorys` (`id`, `name`, `items`) VALUES
(42, 'Marco_Alonso', '[{\"Id\":1,\"Slot\":1,\"Weight\":0,\"Name\":\"AmmoPistol\",\"ImagePath\":\"AmmoPistol.png\",\"Amount\":1}]');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user_vehicles`
--

CREATE TABLE `user_vehicles` (
  `id` int(20) NOT NULL,
  `modelname` varchar(250) NOT NULL,
  `plate` varchar(255) NOT NULL,
  `parked` int(10) NOT NULL DEFAULT 0,
  `fuel` int(100) NOT NULL,
  `owner` varchar(250) NOT NULL,
  `renter` varchar(250) NOT NULL,
  `trunk` varchar(700) NOT NULL DEFAULT '[]',
  `primaryColor` varchar(255) NOT NULL DEFAULT '{"Red":0,"Green":0,"Blue":0,"Alpha":255}',
  `secondaryColor` varchar(255) NOT NULL DEFAULT '{"Red":0,"Green":0,"Blue":0,"Alpha":255}',
  `tuning` varchar(500) NOT NULL DEFAULT '[]'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `user_vehicles`
--

INSERT INTO `user_vehicles` (`id`, `modelname`, `plate`, `parked`, `fuel`, `owner`, `renter`, `trunk`, `primaryColor`, `secondaryColor`, `tuning`) VALUES
(1, 'Oracle', '75771', 1, 90, 'Jack_Hawaii', 'Marco_Alonso', '[]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(2, 'Burrito', '98712', 1, 90, 'Marco_Alonso', '', '[{\"Id\":1,\"Slot\":0,\"Weight\":0,\"Name\":\"Bohrer\",\"ImagePath\":\"drill.png\",\"Amount\":70}]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(3, 'jugular', '17726', 1, 0, 'Marco_Alonso', '', '[]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(4, 'revolter', '19230', 1, 90, 'Marco_Alonso', '', '[]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(5, 'Burrito', '90418', 1, 90, 'Marco_Alonso', '', '[{\"Id\":1,\"Slot\":0,\"Weight\":0,\"Name\":\"Ammoadvancedrifle\",\"ImagePath\":\"AmmoAssaultRifle.png\",\"Amount\":50}]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(6, 'Windsor', 'NOAH', 1, 100, 'Marco_Alonso', '', '[{\"Id\":1,\"Slot\":0,\"Weight\":0,\"Name\":\"Goldbarren\",\"ImagePath\":\"goldbarren.png\",\"Amount\":155},{\"Id\":1,\"Slot\":1,\"Weight\":0,\"Name\":\"Banknoten\",\"ImagePath\":\"Notizblock.png\",\"Amount\":43},{\"Id\":1,\"Slot\":2,\"Weight\":0,\"Name\":\"Pistol\",\"ImagePath\":\"Pistol.png\",\"Amount\":1},{\"Id\":1,\"Slot\":3,\"Weight\":0,\"Name\":\"Sprengstoff\",\"ImagePath\":\"AmmoRPG.png\",\"Amount\":19},{\"Id\":1,\"Slot\":4,\"Weight\":0,\"Name\":\"Bohrer\",\"ImagePath\":\"drill.png\",\"Amount\":19}]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(8, 'zion2', '25510', 1, 0, 'Marco_Alonso', '', '[]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(9, 'specter', '73012', 1, 0, 'Marco_Alonso', '', '[]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(10, 'xa21', '93364', 1, 0, 'Marco_Alonso', '', '[]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(11, 'gburrito', 'TEST', 1, 100, 'Marco_Alonso', 'Jack_Hawaii', '[{\"Id\":1,\"Slot\":0,\"Weight\":0,\"Name\":\"Banknoten\",\"ImagePath\":\"Notizblock.png\",\"Amount\":35}]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]'),
(12, 'jugular', '51858', 1, 0, 'Marco_Alonso', '', '[]', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '{\"Red\":0,\"Green\":0,\"Blue\":0,\"Alpha\":255}', '[]');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `clothingsdata`
--
ALTER TABLE `clothingsdata`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `fraktionen`
--
ALTER TABLE `fraktionen`
  ADD PRIMARY KEY (`fraktionId`);

--
-- Indizes für die Tabelle `fraktion_vehicles`
--
ALTER TABLE `fraktion_vehicles`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `gebiete`
--
ALTER TABLE `gebiete`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `settings`
--
ALTER TABLE `settings`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `user_inventorys`
--
ALTER TABLE `user_inventorys`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `user_vehicles`
--
ALTER TABLE `user_vehicles`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `accounts`
--
ALTER TABLE `accounts`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `clothingsdata`
--
ALTER TABLE `clothingsdata`
  MODIFY `id` int(200) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=187;

--
-- AUTO_INCREMENT für Tabelle `fraktionen`
--
ALTER TABLE `fraktionen`
  MODIFY `fraktionId` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT für Tabelle `fraktion_vehicles`
--
ALTER TABLE `fraktion_vehicles`
  MODIFY `id` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT für Tabelle `gebiete`
--
ALTER TABLE `gebiete`
  MODIFY `id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT für Tabelle `settings`
--
ALTER TABLE `settings`
  MODIFY `id` int(222) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `users`
--
ALTER TABLE `users`
  MODIFY `id` int(222) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT für Tabelle `user_inventorys`
--
ALTER TABLE `user_inventorys`
  MODIFY `id` int(50) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- AUTO_INCREMENT für Tabelle `user_vehicles`
--
ALTER TABLE `user_vehicles`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
