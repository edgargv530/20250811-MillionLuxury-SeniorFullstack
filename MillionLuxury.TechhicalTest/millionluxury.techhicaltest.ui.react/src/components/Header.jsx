import UserInfo from './UserInfo';
export default function Header() {
	const applicationName = 'Properties App'; // You can set this dynamically if needed
	return (
		<div className="row p-2 customBackGround">
			{/* Toggle button */}
			<div className="col-auto px-0 align-content-center">
				<span className="btn btn-outline-light border-0 customItemMenu" data-bs-target="#offcanvasNavbarLight" data-bs-toggle="offcanvas" aria-controls="offcanvasNavbarLight" aria-label="Toggle navigation">
					<i className="fs-2 bi-list"></i>
				</span>
			</div>

			{/* Título de aplicación */}
			<div className="col d-none d-md-block align-content-center">
				<span>Properties App</span>
			</div>

			{/* Iconos informativos */}
			<div className="col text-end align-content-center align-text-bottom">
				{/* Notificaciones */}
				<span className="btn btn-outline-light border-0 customItemMenu">
					<i className="fs-4 bi-bell-fill p-1"></i>
				</span>

				{/* Información del usuario */}
				<span className="dropdown-toggle btn btn-outline-light border-0 customItemMenu" data-bs-toggle="dropdown">
					<i className="fs-3 bi-person-fill"></i>
				</span>
				<div className="dropdown-menu dropdown-menu-end text-center bg-transparent border-0">
					<UserInfo applicationName={applicationName} />
				</div>
			</div>
		</div>
	);
}