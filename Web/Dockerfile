ARG NODE_JS_VERSION
ARG CONTAINER_REGISTRY_SOURCE_LABEL

FROM node:$NODE_JS_VERSION

LABEL org.opencontainers.image.source=CONTAINER_REGISTRY_SOURCE_LABEL

EXPOSE 8080

WORKDIR /app
COPY package*.json ./

RUN npm install 

COPY . .

RUN npm run build

CMD ["npm", "run", "start"]