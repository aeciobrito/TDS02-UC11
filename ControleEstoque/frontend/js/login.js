
document.addEventListener('DOMContentLoaded', () => {
    const token = localStorage.getItem('token');
    const authSection = document.getElementById('auth-section');
    const mainContent = document.getElementById('main-content');
    const navMenu = document.getElementById('nav-menu');
    const btnLogout = document.getElementById('btn-logout');

    if (token) {
        authSection.style.display = 'none';
        mainContent.style.display = 'block';
        navMenu.style.display = 'inline-block';
        btnLogout.style.display = 'inline-block';
    } else {
        authSection.style.display = 'block';
        mainContent.style.display = 'none';
        navMenu.style.display = 'none';
        btnLogout.style.display = 'none';
    }

    const loginForm = document.getElementById('login-form');
    if (loginForm) {
        loginForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            const email = document.getElementById('email').value;
            const senha = document.getElementById('senha').value;
            const errorMsg = document.getElementById('login-error');

            try {
                const response = await fetch(API_BASE_URL + '/api/Usuarios/autenticar', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email, senha })
                });

                if (response.ok) {
                    const data = await response.json();
                    localStorage.setItem('token', data.token);
                    localStorage.setItem('usuario', JSON.stringify(data.usuario));
                    window.location.reload();
                } else {
                    const errorData = await response.json();
                    errorMsg.textContent = errorData.message || 'Erro ao realizar login.';
                    errorMsg.style.display = 'block';
                }
            } catch (err) {
                errorMsg.textContent = 'Erro de conexão.';
                errorMsg.style.display = 'block';
            }
        });
    }
});

function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    window.location.reload();
}
