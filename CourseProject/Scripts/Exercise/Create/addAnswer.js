(function() {
  $(function() {
    return $(document).ready(function() {
      return $('#addAnswer').on('click', function(e) {
        var child, parent;
        e.stopPropagation();
        parent = document.createElement('row');
        parent.className = 'row';
        child = document.createElement('input');
        child.type = 'text';
        child.name = 'answer';
        parent.appendChild(child);
        return $('#answer').append(parent);
      });
    });
  });

}).call(this);

//# sourceMappingURL=addAnswer.js.map
