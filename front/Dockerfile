# Указываем базовый образ
FROM oven/bun:latest

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем файлы проекта
COPY . .

# Устанавливаем зависимости
RUN bun install

# Сборка проекта
RUN bun build

# Экспонируем порт
EXPOSE 3000

# Запускаем приложение
CMD ["bun", "run", "start"]
