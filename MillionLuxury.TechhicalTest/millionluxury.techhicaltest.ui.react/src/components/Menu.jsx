export default function Menu() {
	return (
		<div className="offcanvas offcanvas-start customBackGround" tabIndex="-1" id="offcanvasNavbarLight" aria-labelledby="offcanvasNavbarLightLabel" data-bs-backdrop="static">
			<div className="offcanvas-header">
				<h5 className="offcanvas-title" id="offcanvasNavbarLightLabel">Choose one option</h5>
				<button type="button" className="btn-close btn-close-white" data-bs-dismiss="offcanvas" data-bs-target="#offcanvasNavbarLight" aria-label="Close"></button>
			</div>
			<div className="offcanvas-body">
				<ul className="navbar-nav justify-content-end flex-grow-1 pe-3">
					<li className="nav-item">
						<button className="btn btn btn-outline-light border-0 w-100 text-start customItemMenuDisabled disabled" data-bs-dismiss="offcanvas" data-bs-target="#offcanvasNavbarLight">
							<div className="d-flex">
								<div className="p-1">
									<i className="fs-4 bi-person-fill"></i>
								</div>
								<div className="p-1 align-content-center">Owners</div>
							</div>
						</button>
					</li>
					<li className="nav-item">
						<button className="btn btn btn-outline-light border-0 w-100 text-start customItemMenuDisabled disabled" data-bs-dismiss="offcanvas" data-bs-target="#offcanvasNavbarLight">
							<div className="d-flex">
								<div className="p-1">
									<i className="fs-4 bi-building-fill"></i>
								</div>
								<div className="p-1 align-content-center">Properties</div>
							</div>
						</button>
					</li>
				</ul>
			</div>
		</div>
	);
}