export enum CategoryEnum {
    Physical = "F",
    Special = "S"
}

export const CategoryEnumLabel: Record<CategoryEnum, string> = {
    [CategoryEnum.Physical]: "Physical",
    [CategoryEnum.Special]: "Special"
}