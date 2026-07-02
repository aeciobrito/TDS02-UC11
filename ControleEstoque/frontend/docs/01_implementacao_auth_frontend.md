# Guia Didático: Implementação de Autenticação no Frontend

Este guia detalha os passos práticos para implementar a autenticação baseada em JWT (JSON Web Token) em uma aplicação de interface web (Frontend). O foco é mostrar como o fluxo funciona desde a tela inicial de login até o consumo de rotas protegidas na API.

---

## O Desafio

A nossa API backend agora exige que a maioria das suas rotas (como listar produtos, adicionar fornecedores) seja acessada apenas por usuários autenticados. Para comprovar quem é, o frontend precisa enviar um "crachá" (o Token JWT) a cada requisição.

O fluxo se resume a:
1. O usuário informa credenciais (email e senha).
2. O frontend envia essas credenciais para a API.
3. Se estiverem corretas, a API devolve o **Token**.
4. O frontend guarda esse token no navegador.
5. Em todas as requisições futuras, o frontend anexa esse token no cabeçalho (*Header*) da requisição.

---

## Passo 1: A Porta de Entrada (Tela de Login)

O primeiro passo é garantir que a página inicial (`index.html`) seja o formulário de login. Ela não deve tentar carregar dados da API logo de cara, pois o usuário ainda não tem o token.

**O que foi feito:**
A estrutura do `index.html` apresenta um formulário simples (`#loginForm`) capturando `Email` e `Senha`.

```html
<!-- Exemplo simplificado do HTML de login -->
<form id="loginForm">
    <label>Email</label>
    <input type="email" id="email" required>
    
    <label>Senha</label>
    <input type="password" id="senha" required>
    
    <button type="submit">Entrar</button>
</form>
```

---

## Passo 2: Negociando o Token (`js/login.js`)

Quando o usuário clica em "Entrar", não podemos recarregar a página. Precisamos interceptar o formulário, pegar os dados, mandar para a API, e se der certo, **guardar o token**.

**O que foi feito no JavaScript:**

1.  **Interceptar o Submit:** Usamos `addEventListener('submit', ...)` e `e.preventDefault()`.
2.  **Enviar a Requisição:** Fazemos um `fetch` usando o método `POST` para o endpoint `/api/Usuarios/login`.
3.  **Armazenar Seguro:** Se a API responder com sucesso (Status 200 OK), pegamos o token de dentro da resposta JSON e salvamos no `localStorage` do navegador. O `localStorage` é como um cofre pessoal do site no navegador do usuário, que persiste mesmo se a aba for fechada.

```javascript
// Exemplo didático do login.js
document.getElementById('loginForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const email = document.getElementById('email').value;
    const senha = document.getElementById('senha').value;

    try {
        const response = await fetch('http://localhost:5000/api/Usuarios/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, senha })
        });

        if (response.ok) {
            const data = await response.json();
            // AQUI ESTÁ A MÁGICA: Guardando o token no navegador
            localStorage.setItem('token', data.token); 
            
            // Redireciona para o painel principal após o sucesso
            window.location.href = '/html/produtos/index.html'; 
        } else {
            alert("Credenciais inválidas!");
        }
    } catch (error) {
        console.error("Erro na comunicação com a API");
    }
});
```

---

## Passo 3: Criando Ferramentas Globais (`js/config.js`)

Para não termos que reescrever a mesma lógica de "pegar o token" ou "escrever a URL da API" em todas as telas do sistema, centralizamos isso em um arquivo de configuração global.

**O que foi feito:**
Criamos utilitários que facilitam o trabalho nas outras telas.

```javascript
// js/config.js
const API_BASE_URL = 'http://localhost:5000/api';

// Função utilitária para montar os cabeçalhos padrão
function getHeaders() {
    const headers = {
        'Content-Type': 'application/json'
    };
    
    // Tenta resgatar o token do cofre
    const token = localStorage.getItem('token');
    
    // Se existir token, anexa o cabeçalho Authorization com a palavra Bearer
    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }
    
    return headers;
}
```

---

## Passo 4: Consumindo a API Autenticada (ex: `js/produtos/list.js`)

Agora, quando qualquer tela do sistema for pedir dados para a API (ex: listar os produtos), ela deve usar os cabeçalhos que criamos no `config.js`.

Se não mandarmos o cabeçalho, a API nos retornará um erro **401 Unauthorized** (Não Autorizado).

**O que foi feito nas telas (CRUD):**

1.  Modificamos as chamadas `fetch` para incluir os `headers` com o Token.
2.  Adicionamos uma checagem: Se a API responder com 401, significa que o token expirou ou é inválido. A ação correta é limpar o `localStorage` e mandar o usuário de volta pro login.

```javascript
// Exemplo em js/produtos/list.js
async function carregarProdutos() {
    try {
        const response = await fetch(`${API_BASE_URL}/Produtos`, {
            method: 'GET',
            headers: getHeaders() // Injetando o token aqui!
        });

        if (response.status === 401) {
            // O segurança barrou! Token não existe ou venceu.
            alert('Sessão expirada. Por favor, faça login novamente.');
            localStorage.removeItem('token'); // Limpa a sujeira
            window.location.href = '/index.html'; // Manda pro login
            return;
        }

        if (response.ok) {
            const produtos = await response.json();
            renderizarTabela(produtos);
        }
    } catch (error) {
        console.error('Erro ao carregar dados:', error);
    }
}
```

## Passo 5: Logout (Saindo do sistema)

Sair do sistema no frontend usando JWT é extremamente simples. Como o backend não gerencia a sessão ativamente na memória (a verificação é feita matematicamente pelo token), basta destruirmos o token no navegador do usuário.

```javascript
function realizarLogout() {
    // Apaga a chave do cofre
    localStorage.removeItem('token');
    // Manda de volta para a porta de entrada
    window.location.href = '/index.html';
}
```

---

## Resumo e Resultado

A implementação dessa estratégia transforma o Frontend em um cliente "inteligente" perante a API:
1.  **Segurança:** A senha só trafega uma vez. Nas requisições de dados (como alterar estoque ou ver clientes), o que trafega é o Token.
2.  **Stateless:** O servidor não precisa "lembrar" quem está logado, ele só precisa validar a assinatura do Token recebido no cabeçalho.
3.  **Modularidade:** Encapsular a lógica de headers no `config.js` evitou que o código do token ficasse espalhado e repetido em dezenas de arquivos de listar, editar e excluir.
