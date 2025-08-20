import '../../App.css'
export default function Grid({ columns, response, children }) {
	const pageNumber = Math.ceil((response.Skip / response.Top) + 1);
	const totalPages = Math.ceil(response.TotalRows / response.Top);
	const liPages = [];

	function handlePage(page) {
		console.log(`Page ${page} clicked`);
	}

	for (let i = 1; i <= totalPages; i++) {
		liPages.push(
			<li key={i} className={`page-item ${i === pageNumber ? "disabled" : ""}`} onClick={() => handlePage(i)}>
				<a className="page-link" href="#">{i}</a>
			</li>
		);
	}

	return (
		<div className="table-responsive">
			<table className="table table-bordered table-hover table-striped">
				<caption>
					<div className="d-flex align-items-center justify-content-between">
						<div>{`Page ${pageNumber} of ${totalPages}`}</div>
						<nav aria-label="Page navigation example">
							<ul className="pagination">
								<li className="page-item">
									<a className="page-link" href="#" aria-label="Previous">
										<span aria-hidden="true">&laquo;</span>
									</a>
								</li>
								{liPages}
								{/*<li className="page-item"><a className="page-link" href="#">1</a></li>*/}
								{/*<li className="page-item"><a className="page-link" href="#">2</a></li>*/}
								{/*<li className="page-item"><a className="page-link" href="#">3</a></li>*/}
								<li className="page-item">
									<a className="page-link" href="#" aria-label="Next">
										<span aria-hidden="true">&raquo;</span>
									</a>
								</li>
							</ul>
						</nav>
					</div>
				</caption>
				<thead>
					<tr>
						<th scope="col" className="column-actions">
						</th>
						{columns && columns.map((col, index) => col !== 'Id' && (
							<th key={index} scope="col">
								<div className="d-flex align-items-center justify-content-between">
									<span>{col}</span>
									<div>
										<span className="dropdown-toggle btn btn-outline-dark border-0 customGridMenu" data-bs-toggle="dropdown">
											<i className="fs-6 bi-filter"></i>
										</span>
										<ul className="dropdown-menu border-0">
											<li><a className="dropdown-item" href="#">Add</a></li>
											<li><a className="dropdown-item" href="#">Edit</a></li>
											<li><a className="dropdown-item" href="#">Delete</a></li>
										</ul>
									</div>
								</div>
							</th>
						))}
					</tr>
				</thead>
				<tbody>
					{response.Data && response.Data.map((owner) => (
						<tr key={owner.Id}>
							<td className="column-actions">
								<span className="dropdown-toggle btn btn-outline-dark border-0 customGridMenu" data-bs-toggle="dropdown">
									<i className="fs-6 bi-menu-button-wide-fill"></i>
								</span>
								<ul className="dropdown-menu border-0">
									<li><a className="dropdown-item" href="#">Add</a></li>
									<li><a className="dropdown-item" href="#">Edit</a></li>
									<li><a className="dropdown-item" href="#">Delete</a></li>
								</ul>
							</td>
							{columns && columns.map((col, index) => col !== 'Id' && (<td key={index}>{owner[col]}</td>))}
						</tr>
					))}
				</tbody>
			</table>

		</div>
	);
}