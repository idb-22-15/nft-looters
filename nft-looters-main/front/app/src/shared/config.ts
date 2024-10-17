export const validationErrors = {
  required: 'Это обязательное поле',
  minChars: (minChars: number) => `Минимум символов: ${minChars}`,
  maxChars: (maxChars: number) => `Максимум символов: ${maxChars}`,
}
