namespace App.BL.DTOs;

public record CreateCommitteeDto(
    string NameAz, string NameEn, string NameRu,
    string ChairmanAz, string ChairmanEn, string ChairmanRu,
    string VicePresidentAz, string VicePresidentEn, string VicePresidentRu);

public record UpdateCommitteeDto(
    string NameAz, string NameEn, string NameRu,
    string ChairmanAz, string ChairmanEn, string ChairmanRu,
    string VicePresidentAz, string VicePresidentEn, string VicePresidentRu);

public record CommitteeResponseDto(
    Guid Id,
    string Name,
    string Chairman,
    string VicePresident);
